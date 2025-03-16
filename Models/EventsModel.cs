using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventSphereApp.Models
{
    [Table("EventsFormed")]
    public class EventsModel
    {
        [Key]
        public int FormEventId { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public string Location { get; set; }

        public string About { get; set; }
    }
}
