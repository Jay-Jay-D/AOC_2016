using System;
using DayEleven;

namespace DayElevenTests;

public class DayElevenFacilityTests : IDisposable
{
    public DayElevenFacilityTests()
    {
    }

    public void Dispose() { }

    [Fact]
    public void FacilityHasFourFloors()
    {
        // Given
        // When
        var facility = new Facility();
        // Then
        facility.Floors.Count.Should().Be(4);
    }

    [Fact]
    public void ElevatorStartAtFirstFloor()
    {
        // Given
        // When
        var facility = new Facility();
        // Then
        facility.Elevator.Should().BeOfType<Elevator>();
        facility.Elevator.CurrentFloor.Should().Be(1);
        facility.Elevator.MaxFloor.Should().Be(4);
    }


}