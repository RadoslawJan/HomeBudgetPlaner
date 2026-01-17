using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeBudgetProject.Classes;

namespace HomeBudgetProject.Interfaces
{
    public interface IRaportStrategy
    {
        void GenerateRaport(HomeBudgetPlanner planner);
    }
}
