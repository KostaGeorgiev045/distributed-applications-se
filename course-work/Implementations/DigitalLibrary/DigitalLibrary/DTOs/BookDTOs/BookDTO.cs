using DigitalLibrary.DTOs.ReviewDTOs;
using System.ComponentModel.DataAnnotations;

namespace DigitalLibrary.DTOs.BookDTOs
{
    public class BookDTO
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime PublicationDate { get; set; } 

        public string ISBN { get; set; } 

        public string Genre { get; set; } 

        public decimal Rating { get; set; }

        public List<ReviewDTO> Reviews { get; set; }
    }
}
