

using System;
using Microsoft.AspNetCore.Components;

namespace MagicNumber.Pages
{
    public class NombreMagiqueBase : ComponentBase
    {

		protected const int NBLife = 5;
		protected const int NBMax = 20;
		protected int nbMagic;
		protected int nbVieRestante;
        protected bool? isWinned;
		protected int value;


		protected override void OnInitialized()
		{

            ReinitGame();
			base.OnInitialized();



		}

		protected void ReinitGame()
        {
            var random = new Random();
            nbMagic = random.Next(NBMax);
            nbVieRestante = NBMax;
            isWinned = null;
            value = 0;



        }

        public void state()
        {
            {
                if (value == nbMagic)
                {
                    isWinned = true;
                }
                else
                {
                    nbVieRestante--;
                    if(nbVieRestante == 0)
                    {
                        isWinned = false;
                    }

                }
            }

      

        }
    }
}
