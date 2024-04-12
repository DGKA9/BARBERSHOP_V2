using FakeItEasy;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BARBERSHOP_V2.Test.Helpers
{
    public static class AsyncQueryable
    {
        public static DbSet<T> CreateAsyncDbSet<T>(IList<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = A.Fake<DbSet<T>>(d => d.Implements(typeof(IQueryable<T>)).Implements(typeof(IAsyncEnumerable<T>)));
            A.CallTo(() => ((IQueryable<T>)dbSet).Provider).ReturnsLazily(() => new TestAsyncQueryProvider<T>(queryable.Provider));
            A.CallTo(() => ((IQueryable<T>)dbSet).Expression).ReturnsLazily(() => queryable.Expression);
            A.CallTo(() => ((IQueryable<T>)dbSet).ElementType).ReturnsLazily(() => queryable.ElementType);
            A.CallTo(() => ((IQueryable<T>)dbSet).GetEnumerator()).ReturnsLazily(() => queryable.GetEnumerator());
            A.CallTo(() => ((IAsyncEnumerable<T>)dbSet).GetAsyncEnumerator(A<CancellationToken>._))
                .ReturnsLazily((CancellationToken ct) => new TestAsyncEnumerator<T>(queryable.GetEnumerator()));

            return dbSet;
        }

        internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
        {
            private readonly IQueryProvider _inner;

            internal TestAsyncQueryProvider(IQueryProvider inner)
            {
                _inner = inner;
            }

            public IQueryable CreateQuery(Expression expression)
            {
                return new TestAsyncEnumerable<TEntity>(expression);
            }

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                return new TestAsyncEnumerable<TElement>(expression);
            }

            public object Execute(Expression expression)
            {
                return _inner.Execute(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                return _inner.Execute<TResult>(expression);
            }

            public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
            {
                var resultType = typeof(TResult).GetGenericArguments().Single();
                var executionResult = typeof(IQueryProvider)
                    .GetMethod(name: nameof(IQueryProvider.Execute), genericParameterCount: 1, types: new[] { typeof(Expression) })
                    .MakeGenericMethod(resultType)
                    .Invoke(_inner, new[] { expression });

                return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                    ?.MakeGenericMethod(resultType)
                    .Invoke(null, new[] { executionResult });
            }
        }

        internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
        {
            public TestAsyncEnumerable(IEnumerable<T> enumerable)
                : base(enumerable)
            { }

            public TestAsyncEnumerable(Expression expression)
                : base(expression)
            { }

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
            }
        }

        internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public ValueTask DisposeAsync()
            {
                _inner.Dispose();
                return ValueTask.CompletedTask;
            }

            public T Current => _inner.Current;

            public ValueTask<bool> MoveNextAsync()
            {
                return ValueTask.FromResult(_inner.MoveNext());
            }
        }
    }
}
