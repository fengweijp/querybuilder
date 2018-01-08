using System;
using System.Data;
using System.Data.Common;

namespace SqlKata.Tests.Execution
{
    public interface IExecutionDatabaseFixture : IDisposable
    {
        IDbConnection Connection { get; }
        IDbConnection IsolatedConnection();
    }
}