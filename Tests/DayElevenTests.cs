using DayEleven;

namespace DayElevenTests;

public class DayElevenTests
{
    [Fact]
    public void ElevatorInitialStateIsEmpty()
    {
        // Given
        // When
        var elevator = new Elevator(0);

        // Then
        elevator.IsEmpty.Should().BeTrue();
    }

    [Fact]
    public void ElevatorKnowsItsFloor()
    {
        // Given
        // When
        var elevator = new Elevator(1);

        // Then
        elevator.CurrentFloor.Should().Be(1);
    }

    [Fact]
    public void EmptyElevatorCannotMove()
    {
        // Given
        // When
        var elevator = new Elevator(1);

        // Then
        elevator.Move(1).Should().BeFalse();
    }

    [Fact]
    public void AfterLoadingAnObjectElevatorIsNotEmpty()
    {
        // Given
        var elevator = new Elevator(1);
        var generator = (Type: "generator", Material: "hydrogen");
        var load = new[] { generator };

        // When
        elevator.Load(load);

        // Then
        elevator.IsEmpty.Should().BeFalse();
    }

    [Fact]
    public void AfterLoadingAndUnloadignElevatorIsEmpty()
    {
        // Given
        var elevator = new Elevator(1);
        var generator = (Type: "generator", Material: "hydrogen");
        var load = new[] { generator };
        elevator.Load(load);

        // When
        elevator.Unload(load);

        // Then
        elevator.IsEmpty.Should().BeTrue();
    }

    [Fact]
    public void ElevatorCanLoadUpToTwoObjects()
    {
        // Given
        var elevator = new Elevator(1);
        var generator = (Type: "generator", Material: "hydrogen");
        var chip = (Type: "microchip", Material: "hydrogen");
        var load = new[] { generator, chip };

        // When
        elevator.Load(load).Should().BeTrue();

        // Then
        elevator.Unload(load);
        elevator.IsEmpty.Should().BeTrue();
        load = new[] { generator, chip, chip };
        elevator.Load(load).Should().BeFalse();
    }

    [Fact]
    public void CannotLoadGeneratorWithMicrochipOfDifferentMaterial()
    {
        // Given
        var elevator = new Elevator(1);
        var generator = (Type: "generator", Material: "hydrogen");
        var chip = (Type: "microchip", Material: "lithium");
        var load = new[] { generator, chip };

        // When
        // Then
        elevator.Load(load).Should().BeFalse();
    }

    [Fact]
    public void LoadedElevatorIsLoaded()
    {
        // Given
        var elevator = new Elevator(1);
        var generator = (Type: "generator", Material: "hydrogen");
        var chip = (Type: "microchip", Material: "hydrogen");
        var load = new[] { generator, chip };

        // When
        elevator.Load(load).Should().BeTrue();
        // Then
        elevator.Payload.Should().Equal(load);
    }

    [Fact]
    public void FullElevatorCanUnloadSingleObject()
    {
        // Given
        var elevator = new Elevator(1);
        var generator = (Type: "generator", Material: "hydrogen");
        var chip = (Type: "microchip", Material: "hydrogen");
        var load = new[] { generator, chip };
        elevator.Load(load).Should().BeTrue();
        // When
        elevator.Unload(chip);
        // Then
        elevator.Payload.Should().NotBeEmpty().And.HaveCount(1);
        elevator.Payload.Should().Contain(generator);
    }

    [Fact]
    public void ElevatorCanLoadSingleObjectIfItIsCompatible()
    {
        // Given
        var elevator = new Elevator(1);
        var generator = (Type: "generator", Material: "hydrogen");
        var chip = (Type: "microchip", Material: "hydrogen");
        var load = new[] { generator, chip };
        elevator.Load(load).Should().BeTrue();
        // When
        elevator.Unload(chip);
        // Then
        elevator.Payload.Should().NotBeEmpty().And.HaveCount(1);
        elevator.Payload.Should().Contain(generator);
    }

    [Fact]
    public void ElevatorCannotLoadSingleObjectIfItIsNotCompatible()
    {
        // Given
        var elevator = new Elevator(1);
        var generator = (Type: "generator", Material: "hydrogen");
        var chip = (Type: "microchip", Material: "lithium");
        elevator.Load(generator).Should().BeTrue();
        // When
        // Then
        elevator.Load(chip).Should().BeFalse();
    }

    [Fact]
    public void FullElevatorCannotLoadSingleObject()
    {
        // Given
        var elevator = new Elevator(1);
        var generator = (Type: "generator", Material: "hydrogen");
        var chip = (Type: "microchip", Material: "hydrogen");
        var load = new[] { generator, chip };
        elevator.Load(load).Should().BeTrue();
        // When
        // Then
        elevator.Load(chip).Should().BeFalse();
    }
}