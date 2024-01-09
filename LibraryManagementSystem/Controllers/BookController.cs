using Application.Book;
using Application.Book.CreateBook;
using Application.Book.DeleteBook;
using Application.Book.GetByBookCode;
using Application.Book.GetNotReturnedByUser;
using Application.Book.IssueBook;
using Application.Book.RenewBook;
using Application.Book.ReturnBook;
using Application.Book.SearchBook;
using Application.Book.Update;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseAPIController
    {
        [Authorize(Roles = "Admin")]

        [HttpPost("CreateBook")]
        public async Task<ActionResult> CreateBook(CreateBookCommand command)
        {
            command.LoggedUserName = User.FindFirstValue(ClaimTypes.Name);
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) =>
               NoContent());
        }
          [Authorize(Roles = "Admin")]

        [HttpPut("DeleteBook")]
        public async Task<ActionResult> DeleteBook(DeleteBookCommand command)
        {
            command.LoggedUserName = User.FindFirstValue(ClaimTypes.Name);
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) =>
               NoContent());
        }

        [Authorize(Roles = "Admin")]

        [HttpGet("GetByBookCode")]
        public async Task<ActionResult<List<BookIssueDTO>>> GetByBookCode(string bookCode)
        {
            GetByBookCodeQuery query = new();
            query.BookCode = bookCode;
            var response = await Mediator.Send(query);
            return this.ControllerResponse(response, (response) =>
               Ok(response));
        }
        [Authorize(Roles ="Admin")]

        [HttpGet("GetNotReturnedByUser")]
        public async Task<ActionResult<List<BookIssueDTO>>> GetNotReturnedByUser(string rollNum)
        {

            GetNotReturnedByUserQuery query = new();
            query.RollNum = rollNum;
            var response = await Mediator.Send(query);
            return this.ControllerResponse(response, (response) =>
               Ok(response));
        }

        
        [Authorize(Roles = "Admin")]

        [HttpPost("IssueBook")]
        public async Task<ActionResult> IssueBook(IssueBookCommand command)
        {
            command.LoggedUserName = User.FindFirstValue(ClaimTypes.Name);
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) =>
               NoContent());
        }
        [Authorize(Roles = "Admin")]

        [HttpPut("RenewBook")]
        public async Task<ActionResult<RenewBookDTO>> RenewBook(RenewBookCommand command)
        {
            command.LoggedUserName = User.FindFirstValue(ClaimTypes.Name);
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) =>
               Ok(response));
        }

        [Authorize(Roles = "Admin")]

        [HttpPut("ReturnBook")]
        public async Task<ActionResult> ReturnBook(ReturnBookCommand command)
        {
            command.LoggedUserName = User.FindFirstValue(ClaimTypes.Name);
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) =>
               NoContent());
        }

       // [Authorize(Roles = "Admin")]
      //  [Authorize(Roles = "User")]
        [HttpGet("SearchBook")]
        public async Task<ActionResult<List<BookDTO>>> SearchBook(string keyword)
        {
            SearchBookQuery query = new();
            query.Keyword = keyword;
            var response = await Mediator.Send(query);
            return this.ControllerResponse(response, (response) =>
               Ok(response));

        }


        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateBook")]
        public async Task<ActionResult> UpdateBook(UpdateBookCommand command)
        {
            command.LoggedUserName = User.FindFirstValue(ClaimTypes.Name);
            var response = await Mediator.Send(command);
            return this.ControllerResponse(response, (response) => NoContent());
        }

    }
}
