using NUnit.Framework;
using UniFirst_BL;
using UniFirst_BL.Entities;
using UniFirst_BL.Enums;
using UniFirst_BL.Repository;

namespace UniFirstTest
{
    [TestFixture]
    public class UniFirsttBL_Test
    {
        private VehicleRepository _vehicleRepository;
        private LocationRepository _locationRepository;
        private VehicleTransfer _vehicleTransfer;

        [SetUp]
        public void setup()
        {
            _vehicleRepository  = VehicleRepository.GetRepo();
            _locationRepository = LocationRepository.GetRepo();
            _vehicleTransfer = new VehicleTransfer();

        }

        [Test]
        public void Test_AddVehicle_When_InvalidVin_ShouldReturnFalse()
        {
            //length
            string invalidVin = "ABCDEFGH";
            _vehicleRepository.ClearRepository();

            Assert.IsFalse(_vehicleRepository.AddVehicle("HONDA", "2004", "CR", invalidVin,VehicleTypes.Semi,VehicleStatus.StandBy));

            //Ending with 5 numeric
            invalidVin = "1234567891234567891ABCDE";
            Assert.IsFalse(_vehicleRepository.AddVehicle("HONDA", "2004", "CR", invalidVin, VehicleTypes.Semi, VehicleStatus.StandBy));

            //Minimum of 8 alphas
            invalidVin = "1234abcd1234567891234567";
            Assert.IsFalse(_vehicleRepository.AddVehicle("HONDA", "2004", "CR", invalidVin, VehicleTypes.Semi, VehicleStatus.StandBy));
        }


        [Test]
        public void Test_AddVehicle_When_InvalidYear_ShouldReturnFalse()
        {
            //length
            string invalidYear= "200";
            _vehicleRepository.ClearRepository();

            Assert.IsFalse(_vehicleRepository.AddVehicle("HONDA", invalidYear, "CR", "ABCDEFGH1234567891234567", VehicleTypes.Semi, VehicleStatus.StandBy));
        }

        [Test]
        public void Test_Transfer_When_VehicleObj_Null_ShouldReturnFalse()
        {
            _vehicleRepository.ClearRepository();
            _locationRepository.ClearRepository();
            _vehicleRepository.AddVehicle("HONDA", "2015", "CR", "ABCDEFGH1234567891234567", VehicleTypes.Semi, VehicleStatus.Repair);

            _locationRepository.AddLocation(1, LocationType.Branch);
            _locationRepository.AddLocation(2, LocationType.Branch);

            Location fromLocation = _locationRepository.GetLocationById(1);
            Location toLocation = _locationRepository.GetLocationById(2);
            
            //Invaild Vin, Transfer should fail
            Assert.IsFalse(_vehicleTransfer.Transfer(fromLocation.LocationId, toLocation.LocationId, "ABCDEFGH1234567891234522"));
        }

        [Test]
        public void Test_Transfer_When_Location_Null_Should_ReturnFalse()
        {
            _vehicleRepository.ClearRepository();
            _locationRepository.ClearRepository();
            _vehicleRepository.AddVehicle("HONDA", "2015", "CR", "ABCDEFGH1234567891234567", VehicleTypes.Semi, VehicleStatus.Repair);

            _locationRepository.AddLocation(1, LocationType.Branch);
            _locationRepository.AddLocation(2, LocationType.Branch);


            //Invalid location
            Assert.IsFalse(_vehicleTransfer.Transfer(5, 2, "ABCDEFGH1234567891234567"));
        }

        [Test]
        public void Test_Transfer_Only_Vehicles_StandBy_CanBe_Moved__ShouldReturnFalse()
        {
            _vehicleRepository.ClearRepository();
            _locationRepository.ClearRepository();
            _vehicleRepository.AddVehicle("HONDA", "2015", "CR", "ABCDEFGH1234567891234567", VehicleTypes.Semi, VehicleStatus.Repair);

            _locationRepository.AddLocation(1, LocationType.Branch);
            _locationRepository.AddLocation(2, LocationType.Branch);

            //Only vehicles in stand-by can be moved
            Assert.IsFalse(_vehicleTransfer.Transfer(1, 2, "ABCDEFGH1234567891234567"));
        }

        [Test]
        public void Test_Transfer_Semi_Cannot_Be_Transfered_Between_Branches_ShouldReturnFalse()
        {
            _vehicleRepository.ClearRepository();
            _locationRepository.ClearRepository();
            _vehicleRepository.AddVehicle("KIA", "2015", "CRV", "DDCDEFGH1234567891234567", VehicleTypes.Semi, VehicleStatus.StandBy);

            _locationRepository.AddLocation(1, LocationType.Branch);
            _locationRepository.AddLocation(2, LocationType.DistributionCenter);

            //Semi can be transferred between distribution centers but no branches
            Assert.IsFalse(_vehicleTransfer.Transfer(1, 2, "DDCDEFGH1234567891234567"));
        }
        }
}
