using System;
using s18085_egz1.Controllers;
using s18085_egz1.Model;

namespace s18085_egz1.Services
{
    public interface IDbService
    {
        Mediciment FindMedicimentsWithPrescr(string id);
        bool DeletePatient(string id);
    }
}
