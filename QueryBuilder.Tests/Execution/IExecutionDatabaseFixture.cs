using System;
using System.Data.Common;

namespace SqlKata.Tests.Execution
{
    public interface IExecutionDatabaseFixture : IDisposable
    {
        DbConnection Connection { get; }
        DbConnection IsolatedConnection();
    }
}