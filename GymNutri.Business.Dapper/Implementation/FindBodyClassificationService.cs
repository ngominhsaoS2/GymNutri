using Dapper;
using GymNutri.Business.Dapper.Interfaces;
using GymNutri.Data.Entities;
using GymNutri.Utilities.Constants;
using GymNutri.Utilities.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace GymNutri.Business.Dapper.Implementation
{
    public class FindBodyClassificationService : IFindBodyClassificationService
    {
        private readonly IConfiguration _configuration;

        public FindBodyClassificationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<GenericResult> FindBodyClassification(int userBodyIndexId, int bodyClassificationId)
        {
            bool result = true;
            string message = string.Empty;
            bool data;

            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@userBodyIndexId", userBodyIndexId);
                dynamicParameters.Add("@bodyClassificationId", bodyClassificationId);
                dynamicParameters.Add("@correct", result, DbType.Int32, ParameterDirection.Output);
                try
                {
                    await sqlConnection.QueryAsync("FindBodyClassification", dynamicParameters, commandType: CommandType.StoredProcedure);
                    int correct = dynamicParameters.Get<int>("@correct");
                    data = correct == 1 ? true : false;
                    message = CommonConstants.Message.Success;
                }
                catch (Exception ex)
                {
                    data = false;
                    result = false;
                    message = ex.Message;
                }

                return new GenericResult(result, message, data);
            }
        }

        public async Task<bool> FindBodyClassification2(int userBodyIndexId, int bodyClassificationId)
        {
            int result = 0;
            using (var sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await sqlConnection.OpenAsync();
                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@userBodyIndexId", userBodyIndexId);
                dynamicParameters.Add("@bodyClassificationId", bodyClassificationId);
                dynamicParameters.Add("@result", result, DbType.Int32, ParameterDirection.Output);
                try
                {
                    await sqlConnection.QueryAsync("FindBodyClassification", dynamicParameters, commandType: CommandType.StoredProcedure);
                    result = dynamicParameters.Get<int>("@result");
                    return result == 1 ? true : false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }


    }
}
