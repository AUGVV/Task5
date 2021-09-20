using Game;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task5.Models;

namespace Task5.Hubs
{
    public class GameHub : Hub
    {
        public async Task Send(string message, string userName)
        {
            await Clients.All.SendAsync("Send", message, userName);
        }



 

        public GameHub()
        {

           
        


        }
        static List<StepModel> Steps = new List<StepModel>();

 
        public async void Step(string StepChoise, string Who, string GameId, string CountStr, string CreatorName, string ConnectorName )
        {
            string[] Conditions = new string[] { "Камень", "Ножницы", "Бумага" };
            Rules rules = new Rules(Conditions);
  
            int CountSteps = Convert.ToInt32(CountStr);
            if(CountSteps == 3)
            {
                Conditions = new string[] { "Камень", "Ножницы", "Бумага" };
                rules = new Rules(Conditions);
            }
            else if(CountSteps == 5)
            {
                Conditions = new string[] { "Камень", "Ножницы", "Бумага", "Спок", "Кок" };
                rules = new Rules(Conditions);
            }
            else if(CountSteps == 7)
            {
                Conditions = new string[] { "Камень", "Ножницы", "Бумага", "Спок", "Кок", "Рок", "Пок" };
                rules = new Rules(Conditions);
            }


            if(Steps.Where(x=>x.GameId == GameId).FirstOrDefault() == null)
            {
                if (Who == "Creator")
                {
                    Debug.WriteLine($"aadv Creator");
                    Steps.Add(new StepModel() { GameId = GameId, CreatorChoise = StepChoise });
                }
                else if(Who == "Connection")
                {
                    Steps.Add(new StepModel() { GameId = GameId, ConnectorChoise = StepChoise });
                    Debug.WriteLine($"aadv Connection");
                }
            }
            else if(Steps.Where(x => x.GameId == GameId).FirstOrDefault().GameId == GameId)
            {
                int CompanionChoise = 0;
                int CreatorChoise = 0;
                if (Who == "Creator")
                {
                   for (int i = 0; i< Conditions.Length; i++)
                   {
                        if(Conditions[i] == Steps.Where(x => x.GameId == GameId).FirstOrDefault().ConnectorChoise)
                        {
                            break;
                        }
                        else
                        {
                            CompanionChoise++;
                        }
                   }
                    for (int i = 0; i < Conditions.Length; i++)
                    {
                        if (Conditions[i] == StepChoise)
                        {
                            break;
                        }
                        else
                        {
                            CreatorChoise++;
                        }
                    }
                    string result = rules.GiveWinner(CreatorChoise, CompanionChoise, CreatorName, ConnectorName);
                    await Clients.All.SendAsync($"{GameId}Result", result);
                }
                else if (Who == "Connection")
                {
                    for (int i = 0; i < Conditions.Length; i++)
                    {
                        if (Conditions[i] == Steps.Where(x => x.GameId == GameId).FirstOrDefault().CreatorChoise)
                        {
                            break;
                        }
                        else
                        {
                            CreatorChoise++;
                        }
                    }
                    for (int i = 0; i < Conditions.Length; i++)
                    {
                        if (Conditions[i] == StepChoise)
                        {
                            break;
                        }
                        else
                        {
                            CompanionChoise++;
                        }
                    }
                    string result = rules.GiveWinner(CreatorChoise, CompanionChoise, CreatorName, ConnectorName);
                    await Clients.All.SendAsync($"{GameId}Result", result);

                }
            }

            Debug.WriteLine($"{Who} doing step {StepChoise}");



        }


        public async Task ConnectedName(string UserName, string CreatorName, string GameId)
        {
            if (UserName == CreatorName)
            {
                await Clients.All.SendAsync(GameId, "");
            }
            else
            {
                await Clients.All.SendAsync(GameId, UserName);
            }
        }



        public string Connect(string userName)
        {
            return userName;
        }

    }
}
