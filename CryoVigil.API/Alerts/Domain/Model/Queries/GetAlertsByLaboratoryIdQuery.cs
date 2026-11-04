namespace CryoVigil.API.Alerts.Domain.Model.Queries;

public class GetAlertsByLaboratoryIdQuery(int laboratoryId)
{
    public int LaboratoryId { get; set; }
}