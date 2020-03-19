using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace lab4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolveController : ControllerBase
    {   
        [HttpPost]
        public string Solve([FromForm]float[][] equation)
        {
            double[,] matrix1 = new double[equation.GetLength(0), equation[0].Length];
            List<List<float>> matrix2 = new List<List<float>>();
            for (int i=0;i<equation.Length;i++)
            {
                for(int j=0;j<equation[i].Length;j++)
                {
                    matrix1[i, j] = equation[i][j];
                }
            }
            Solver solver = new Solver(matrix1);
            return solver.Solve();
        }
    }
}