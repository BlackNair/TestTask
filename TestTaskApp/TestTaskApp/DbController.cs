using Dapper;
using Microsoft.AspNetCore.Mvc;
using TestTaskConfig;
using TestTaskConfig.Enums;
using TestTaskConfig.Models;
using TestTaskCore.Interfaces;

namespace TestTaskApp
{
    public class DbController:Controller
    {
        private readonly IDbWorker _dbWorker;

        private readonly ILogger<DbController> _logger;

        public DbController(IDbWorker dbWorker, ILogger<DbController> logger) 
        {
            _dbWorker= dbWorker;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            _logger.LogInformation("BEGIN: GetUsers");
            List<UserModel> users = new List<UserModel>();
            try
            {
                users = _dbWorker.ExecuteProcGet<UserModel>(SqlExec.GetUsersProc);
            }
            catch (Exception ex) 
            {
                _logger.LogError($"ERROR: {ex.Message}");
                return Ok(ex.Message);
            }
            finally
            {
                _logger.LogInformation("END: GetUsers");
            }
            return Ok(users);
        }

        [HttpPut]
        public IActionResult InsertUsers([FromQuery] List<UserModel> users) 
        {
            _logger.LogInformation("BEGIN: InsertUsers");
            try
            {
                foreach (var user in users) 
                { 
                    DynamicParameters param = new DynamicParameters();
                    param.Add("FirstName", user.FirstName);
                    param.Add("SecondName", user.SecondName);
                    param.Add("Surname", user.Surname);
                    param.Add("Phone", user.Phone);
                    param.Add("Birhday", user.Birthday);
                    _dbWorker.ExecuteProc(SqlExec.InsertUsersProc, param);
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError($"ERROR: {ex.Message}");
                return Ok(ex.Message);
            }
            finally
            {
                _logger.LogInformation("END: InsertUsers");
            }
            return Ok("Succes");
        }

        [HttpPost]
        public IActionResult ChangeConnect([FromQuery] ServerEnum server, DbEnum db)
        {
            _logger.LogInformation("BEGIN: ChangeConnect");
            try
            {
                _dbWorker.SetConnect(db, server);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: {ex.Message}");
                return Ok(ex.Message);
            }
            finally
            {
                _logger.LogInformation("END: ChangeConnect");
            }
            return Ok("Succes");
        }
    }
}
