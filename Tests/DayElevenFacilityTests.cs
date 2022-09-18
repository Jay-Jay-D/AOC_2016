using System;
using DayEleven;

namespace DayElevenTests;

public class DayElevenFacilityTests : IDisposable
{
    Facility _facility;
    RtgComponent _hydrogenGenerator = new RtgComponent("generator", "hydrogen");
    RtgComponent _hydrogenChip = new RtgComponent("microchip", "hydrogen");
    RtgComponent[] _hydrogenGeneratorAndChip;
    RtgComponent _lithiumChip = new RtgComponent("microchip", "lithium");

    public DayElevenFacilityTests()
    {
        _facility = new Facility();
        _hydrogenGeneratorAndChip = new[] { _hydrogenGenerator, _hydrogenChip };
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
    public void FacilityFloorAreEmptyByDefault()
    {
        _facility[1].Should().BeOfType<Floor>();
        _facility[1].Count.Should().Be(0);
        _facility[1].IsEmpty.Should().BeTrue();
    }

    [Fact]
    public void FacilityFloorsHaveStuff()
    {
        // Given
        var floors = new Dictionary<int, Floor>{
            {1, new Floor()},
            {2, new Floor(new[] {_hydrogenChip})},
            {3, new Floor(new[] {_lithiumChip})},
            {4, new Floor(_hydrogenGeneratorAndChip)},
        };
        // When
        var facility = new Facility(floors);
        // Then
        facility[1].IsEmpty.Should().BeTrue();
        facility[2].IsEmpty.Should().BeFalse();
        facility[2].Payload.Should().BeEquivalentTo(new[] { _hydrogenChip });
        facility[3].IsEmpty.Should().BeFalse();
        facility[3].Payload.Should().BeEquivalentTo(new[] { _lithiumChip });
        facility[4].IsEmpty.Should().BeFalse();
        facility[4].Payload.Should().BeEquivalentTo(_hydrogenGeneratorAndChip);
    }

    [Fact(Skip = "WIP")]
    public void ElevatorCanLoadComponentFromFloor()
    {
        // Given
        var floors = new Dictionary<int, Floor>{
            {1, new Floor(_hydrogenGeneratorAndChip)},
            {2, new Floor()},
            {3, new Floor()},
            {4, new Floor()},
        };
        var facility = new Facility(floors);
        // When
        facility.LoadElevator(new[] { _hydrogenGenerator }).Should().BeTrue();
        // Then
        facility[1].IsEmpty.Should().BeFalse();
        facility[1].Payload.Should().BeEquivalentTo(new[] { _hydrogenChip });
        facility.Elevator.IsEmpty.Should().BeFalse();
        facility.Elevator.Payload.Should().BeEquivalentTo(new[] { _hydrogenGenerator });
    }
}