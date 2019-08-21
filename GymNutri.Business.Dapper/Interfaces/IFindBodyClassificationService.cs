using GymNutri.Data.Entities;
using GymNutri.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GymNutri.Business.Dapper.Interfaces
{
    public interface IFindBodyClassificationService
    {
        Task<GenericResult> FindBodyClassification(int userBodyIndexId, int bodyClassificationId);

        Task<bool> FindBodyClassification2(int userBodyIndexId, int bodyClassificationId);

    }
}
