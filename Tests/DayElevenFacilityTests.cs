using System;
using DayEleven;

namespace DayElevenTests;

public class DayElevenFacilityTests : IDisposable
{
    Facility _facility;

    public DayElevenFacilityTests()
    {
        _facility = new Facility();
    }

    public void Dispose() { }

    [Fact]
    public void FacilityHasFourFloors()
    {
        _facility.Floors.Count.Should().Be(4);
    }

    [Fact]
    public void ElevatorStartAtFirstFloor()
    {
        _facility.Elevator.Should().BeOfType<Elevator>();
        _facility.Elevator.CurrentFloor.Should().Be(1);
        _facility.Elevator.MaxFloor.Should().Be(4);
    }

    [Fact]
    public void EmptyFloorAreEmpty()
    {
        _facility[1].Should().BeOfType<Floor>();
        _facility[1].Count.Should().Be(0);
    }


}