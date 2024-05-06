using Domain.Entities;

namespace WebUI.Features.CarRaces
{
    public class CarRaceService
    {
        public CarRace RunRace(CarRace carRace)
        {
            var racers = new List<Car>();
            foreach (var car in carRace.Cars)
            {
                while (car.DistanceCoveredInMiles < carRace.Distance
                    && car.RacedForHours < carRace.TimeLimit)
                {
                    var random = new Random().Next(1, 101);

                    if(random <= car.MalfunctionChance)
                    {
                        car.MalfunctionsOcurred++;
                    }
                    else 
                    {
                        car.DistanceCoveredInMiles += car.Speed;
                    }

                    car.RacedForHours++;
                }

                if (car.DistanceCoveredInMiles >= carRace.Distance)
                {
                    car.FinishedRace = true;
                }

                racers.Add(car);
            }

            carRace.Cars = racers.OrderBy(x => x.FinishedRace)
                .ThenByDescending(x => x.DistanceCoveredInMiles)
                .ThenByDescending(x => x.RacedForHours)
                .ToList();

            return carRace;
        }
    }
}
