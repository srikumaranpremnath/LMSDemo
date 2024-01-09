using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Book
{
    public class BookDTO
    {
        public Guid BookDetailsId { get; set; }
        public string BookName { get; set; }
        public int RackId { get; set; }
        public int RowId { get; set; }
        public string BookStatus { get; set; }
        public BookDescriptionDTO BookDescription { get; set; }

    }
    public class BookDescriptionDTO
    {
        public Guid BookDescriptionId { get; set; }
        public string AuthorName { get; set; }
        public string PublicationName { get; set; }
        public int? EditionNumber { get; set; }
    }
}
