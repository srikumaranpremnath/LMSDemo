using Application.Book;
using Application.Book.GetActivityByUser;
using Application.User;
using Application.User.BatchDelete;
using Application.User.ChangePassword;
using Application.User.CreateUser;
using Application.User.DeleteUser;
using Application.User.GetBatchByNotReturned;
using Application.User.GetByRollNum;
using Application.User.ResetPassword;
using Application.User.UpdateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    public class UserController : BaseAPIController
    {
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        [HttpPut("ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordCommand command)
        {
            command.LoggedUserName = User.FindFirstValue(ClaimTypes.Name);
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) =>
             NoContent());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser(CreateUserCommand command)
        {
            command.LoggedUserName = User.FindFirstValue(ClaimTypes.Name);
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) =>
               NoContent());
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("DeleteUser")]
        public async Task<ActionResult> DeleteUser(DeleteUserCommand command)
        {
            command.LoggedUserName = User.FindFirstValue(ClaimTypes.Name);
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) =>
               NoContent());
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("DeleteByBatch")]
        public async Task<ActionResult> DeleteByBatch(DeleteByBatchCommand command)
        {
            command.LoggedUserName = User.FindFirstValue(ClaimTypes.Name);
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) =>
               NoContent());
        }

        [Authorize(Roles = "User")]
        [HttpGet("GetActivityByUser")]
        public async Task<ActionResult<List<BookIssueDTO>>> GetActivityByUser(string rollNum)
        {
            GetActivityByUserQuery query = new();
            query.RollNum = rollNum;
            var response = await Mediator.Send(query);
            return this.ControllerResponse(response, (response) =>
               Ok(response));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetBatchByNotReturned")]
        public async Task<ActionResult<List<UserDTO>>> GetBatchByNotReturned(string batchYear)
        {
            GetBatchByNotReturnedQuery query = new();
            query.batchYear = batchYear;
            var response = await Mediator.Send(query);
            return this.ControllerResponse(response, (response) =>
               Ok(response));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetByRollNum")]
        public async Task<ActionResult<List<UserDTO>>> GetByRollNum(string rollNum)
        {
            GetByRollNumQuery query = new();
            query.RollNum = rollNum;
            var response = await Mediator.Send(query);
            return this.ControllerResponse(response, (response) =>
               Ok(response));
        }
        
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        [HttpPut("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordCommand command)
        {
            command.LoggedUserName = User.FindFirstValue(ClaimTypes.Name);
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) =>
               NoContent());
        }
        
        
        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser(UpdateUserCommand command)
        {
            command.LoggedUserName = User.FindFirstValue(ClaimTypes.Name);
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) =>
              NoContent());
        }
        

    }
}
