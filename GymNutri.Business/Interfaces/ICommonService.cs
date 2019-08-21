using System;
using System.Collections.Generic;
using System.Text;
using GymNutri.Data.ViewModels.Common;

namespace GymNutri.Business.Interfaces
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();

        List<SlideViewModel> GetSlides(string groupAlias);

        SystemConfigViewModel GetSystemConfig(string code);
    }
}
