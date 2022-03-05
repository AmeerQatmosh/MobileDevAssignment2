using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections; 
namespace Asynchronous_Programming_Assignment_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("                        Asynchronous Programming Concole Application\n");
            UserInput userInput = new UserInput();
            List<Route> routes = userInput.getRoutes();
            Console.WriteLine(routes.Count.ToString());
            Route bestRoute = new Calculations(routes).syncCalculation();
            Console.WriteLine("Shortest path is: \n");
            // bestRoute.printRouters();
            Thread T = new Thread(new ThreadStart(bestRoute.printRouters));
            T.Start();



        }

        public class Router
        {
            int time;
            public Router(int time)
            {
                this.time = time;
            }

            public int getTime()
            {
                return time;
            }

        }

        public class Route
        {
            public int id;
            public List<Router> routers = new List<Router>();
            public Route(int id, List<Router> routers)
            {
                this.id = id;
                this.routers = routers;
            }

            public int getPathTime() {
                int sum = 0;
                foreach (Router router in routers)
                {
                    sum += router.getTime();
                }
                return sum;
            }


            public void printRouters()
            {
                Console.WriteLine("\nRoute time = " + getPathTime().ToString());
                foreach (Router router in routers)
                {
                    Console.Write(router.getTime().ToString() + ",");
                }

            }
        }


        class UserInput
        {

            public List<Route> routeArr = new List<Route>();
            public List<Router> routersArr;

            void print_menu()

            {

                Console.WriteLine("               ========================================================================");
                Console.WriteLine("               | To perform the following operations, choose the operation number.    |");
                Console.WriteLine("               ========================================================================");
                Console.WriteLine("               | 1. Create a new route                                                |");
                Console.WriteLine("               | 2. Add a router to given route                                       |");
                Console.WriteLine("               | 3. Find shortest path using Sync method.                             |");
                Console.WriteLine("               ========================================================================");
                Console.WriteLine("\n");
                Console.WriteLine("Enter your choice: ");

            }

            public List<Route> getRoutes()
            {
                while (true)
                {
                    print_menu();
                    int operation = Convert.ToInt32(Console.ReadLine());
                    switch (operation)
                    {

                        case 1:
                            Console.WriteLine("Please Enter the routers of the route");
                            getRoutesValues();
                            break;

                        case 2:
                            addRouterToSpecificRoute();
                            break;

                        case 3:
                            return routeArr;
                     

                        default:
                            Console.WriteLine("*Unknown choice, Please try again! ");
                            break;

                    }
                }
            }

            void getRoutesValues()
            {
                Array arr = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), Convert.ToInt32);
                routersArr = new List<Router>();
                foreach (int item in arr)
                {
                    var router = new Router(item);
                    routersArr.Add(router);

                }
                Route route = new Route(routeArr.Count, routersArr);
                Console.WriteLine("new route created with id = " + route.id);
                routeArr.Add(route);

            }

            public void addRouterToSpecificRoute()
            {
                Console.WriteLine("Please enter the Route id: \n");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please enter the value of router: \n");
                int routerTime = Convert.ToInt32(Console.ReadLine());

                Route item = routeArr.FirstOrDefault(o => o.id == id);
                if (item != null)
                {
                    item.routers.Add(new Router(routerTime));

                }

            }

        }

        class Calculations
        {
            //Method A: Sync method that calculates best route from available routes.
            //Method B: Async method that calculates best route asynchronously by calculating total time required for each route in a separate thread.
            //  public async Task<string> CalculateByAsync() {}
            List<Route> routeArr = new List<Route>();
            int min;
            Route bestRoute;
            public Calculations(List<Route> routes)
            {
                this.routeArr = routes;
            }

            public Route syncCalculation()
            {
                if (routeArr.Count > 0)
                {
                    min = routeArr[0].getPathTime();
                    bestRoute = routeArr[0];
                    foreach (Route route in routeArr)
                    {
                        Thread.Sleep(100);

                        var routePathTime = route.getPathTime();
                        if (routePathTime < min)
                        {

                            min = routePathTime;
                            bestRoute = route;
                        }
                    }
                    return bestRoute;

                } else {
                    return null;

                }
            }

            /* public async static void asyncCalculation()
             {
                 var routes = new List<Route>()
                 {
                    // await Task.Delay(1000);
                 }


             }
         }
            */


        }
    }
    }
