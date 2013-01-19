using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Attendance.Core.Domain;
using Attendance.Core.Infrastructure.Azure;
using Microsoft.WindowsAzure.Storage.Table;

namespace Attendance.Core.Repositories
{
	public interface IPunchRepository
	{
		IEnumerable<Punch> GetPunchesForEmployee(string employeeId);
		IEnumerable<Punch> GetPunchesUnderTime(string companyId, TimeSpan timeSpan);
		IEnumerable<Punch> GetPunchesOverTime(string companyId, TimeSpan timeSpan);
		Punch Get(string serialNr, string rowKey);
	}

	public class PunchRepository : IPunchRepository
	{
        private readonly ITableReference<Punch> _tableReference;

		public PunchRepository(ITableReference<Punch> tableReference)
        {
            _tableReference = tableReference;
        }

		public IEnumerable<Punch> GetPunchesForEmployee(string employeeId)
		{
			TableQuery<Punch> partitionQuery = new TableQuery<Punch>().Where(
				TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, employeeId));

            return _tableReference.ExecuteQuery(partitionQuery);
		}

		public IEnumerable<Punch> GetPunchesUnderTime(string companyId, TimeSpan timeSpan)
		{
			TableQuery<Punch> partitionQuery = new TableQuery<Punch>();
			var allResults = _tableReference.ExecuteQuery(partitionQuery);

			allResults = allResults.Where(r => r.TimeOut - r.TimeIn < timeSpan);

			return allResults;
		}

		public IEnumerable<Punch> GetPunchesOverTime(string companyId, TimeSpan timeSpan)
		{
			TableQuery<Punch> partitionQuery = new TableQuery<Punch>();
			var allResults = _tableReference.ExecuteQuery(partitionQuery);

			allResults = allResults.Where(r => r.TimeOut - r.TimeIn > timeSpan);

			return allResults;
		}

		public Punch Get(string serialNr, string rowKey)
		{
			var operation = TableOperation.Retrieve<Punch>(serialNr, rowKey);
			var result = _tableReference.Execute(operation);
			return result.Result as Punch;
		}
	}
}
