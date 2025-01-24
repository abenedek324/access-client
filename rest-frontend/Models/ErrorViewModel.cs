/*!
 * A frontend részeinek modeljeit tartalmazó névtér
 */
namespace rest_frontend.Models
{
    /*!
    * Hiba megjelenítését definiáló osztály
    */
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
