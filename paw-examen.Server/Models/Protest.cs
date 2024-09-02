namespace paw_examen.Server.Models
{
    public class Protest
    {
        public int Id { get; set; }
        public string Denumire {  get; set; }
        public string Descriere { get; set; }
        public DateTime Data {  get; set; }
        public int Numar_participanti {  get; set; }

        public int LocatieId { get; set; }
        public Locatie Locatie { get; set; }
    }
}
