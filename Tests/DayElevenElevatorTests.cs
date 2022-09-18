using System;
using DayEleven;

namespace DayElevenTests;

public class DayElevenElevatorTests : IDisposable
{
    Elevator _elevator;
    RTGM _hydrogenGenerator =new RTGM("generator", "hydrogen");
    RTGM _hydrogenChip = new RTGM("microchip", "hydrogen");
    RTGM[] _hydrogenGeneratorAndChip;
    RTGM _lithiumChip = new RTGM("microchip", "lithium");

    public DayElevenElevatorTests()
    {
        _elevator = new Elevator(1);
        _hydrogenGeneratorAndChip = new[] { _hydrogenGenerator, _hydrogenChip };
    }


    public void Dispose()
    { }


    [Fact]
    public void ElevatorInitialStateIsEmpty()
    {
        _elevator.IsEmpty.Should().BeTrue();
    }

    [Fact]
    public void ElevatorKnowsItsFloor()
    {
        _elevator.CurrentFloor.Should().Be(1);
    }

    [Fact]
    public void EmptyElevatorCannotMove()
    {
        _elevator.Move(1).Should().Be(1);
    }

    [Fact]
    public void AfterLoadingAnObjectElevatorIsNotEmpty()
    {
        // When
        _elevator.Load(_hydrogenChip);

        // Then
        _elevator.IsEmpty.Should().BeFalse();
    }

    [Fact]
    public void AfterLoadingAndUnloadignElevatorIsEmpty()
    {
        // Given
        _elevator.Load(_hydrogenGeneratorAndChip);

        // When
        _elevator.Unload(_hydrogenGeneratorAndChip);

        // Then
        _elevator.IsEmpty.Should().BeTrue();
    }

    [Fact]
    public void ElevatorCanLoadUpToTwoObjects()
    {
        // Given
        // When
        _elevator.Load(_hydrogenGeneratorAndChip).Should().BeTrue();

        // Then
        _elevator.Load(_hydrogenGenerator).Should().BeFalse();
    }

    [Fact]
    public void CannotLoadGeneratorWithMicrochipOfDifferentMaterial()
    {
        // Given
        var load = new[] { _hydrogenGenerator, _lithiumChip };

        // When
        // Then
        _elevator.Load(load).Should().BeFalse();
    }

    [Fact]
    public void LoadedElevatorIsLoaded()
    {
        // Given
        // When
        _elevator.Load(_hydrogenGeneratorAndChip).Should().BeTrue();
        // Then
        _elevator.Payload.Should().Equal(_hydrogenGeneratorAndChip);
    }

    [Fact]
    public void FullElevatorCanUnloadSingleObject()
    {
        // Given
        _elevator.Load(_hydrogenGeneratorAndChip).Should().BeTrue();
        // When
        _elevator.Unload(_hydrogenChip);
        // Then
        _elevator.Payload.Should().NotBeEmpty().And.HaveCount(1);
        _elevator.Payload.Should().Contain(_hydrogenGenerator);
    }

    [Fact]
    public void ElevatorCannotLoadSingleObjectIfItIsNotCompatible()
    {
        // Given
        _elevator.Load(_hydrogenGenerator).Should().BeTrue();
        // When
        // Then
        _elevator.Load(_lithiumChip).Should().BeFalse();
    }

    [Fact]
    public void FullElevatorCannotLoadSingleObject()
    {
        // Given
        _elevator.Load(_hydrogenGeneratorAndChip).Should().BeTrue();
        // When
        // Then
        _elevator.Load(_hydrogenChip).Should().BeFalse();
    }

    [Fact]
    public void ElevatorKnowsHowManyFloorsThereAre()
    {
        // Given
        var elevator = new Elevator(2, 6);
        // When
        // Then
        elevator.CurrentFloor.Should().Be(2);
        elevator.MaxFloor.Should().Be(6);
    }

    [Fact]
    public void ElevatorMoves()
    {
        // Given
        var elevator = new Elevator(2, 6);
        elevator.Load(_hydrogenChip).Should().BeTrue();
        // When
        // Then
        elevator.Move(3).Should().Be(5);
        elevator.Move(-1).Should().Be(4);
    }
}
