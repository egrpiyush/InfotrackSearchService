using Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface
{
    public interface IGoogleSearch
    {
        List<int> Search(string searchFor, string urlOfInterest);
    }
}
