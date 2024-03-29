using System;
using System.Collections;
using DayEleven;

namespace DayElevenTests;



public class DayElevenFacilityTests : IDisposable
{
    Facility _facility;

    public DayElevenFacilityTests()
    {
        _facility = new Facility();
    }

    public void Dispose()
    { }

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
            {2, new Floor(new[] {TestComponents.HydrogenChip})},
            {3, new Floor(new[] {TestComponents.LithiumChip})},
            {4, new Floor(TestComponents.HydrogenGeneratorAndChip)},
        };
        // When
        var facility = new Facility(floors);
        // Then
        facility[1].IsEmpty.Should().BeTrue();
        facility[2].IsEmpty.Should().BeFalse();
        facility[2].Payload.Should().BeEquivalentTo(new[] { TestComponents.HydrogenChip });
        facility[3].IsEmpty.Should().BeFalse();
        facility[3].Payload.Should().BeEquivalentTo(new[] { TestComponents.LithiumChip });
        facility[4].IsEmpty.Should().BeFalse();
        facility[4].Payload.Should().BeEquivalentTo(TestComponents.HydrogenGeneratorAndChip);
    }

    [Fact]
    public void ElevatorCanLoadComponentFromFloor()
    {
        // Given
        var floors = new Dictionary<int, Floor>{
            {1, new Floor(TestComponents.HydrogenGeneratorAndChip)},
            {2, new Floor()},
            {3, new Floor()},
            {4, new Floor()},
        };
        var facility = new Facility(floors);
        // When
        facility.LoadElevator(new[] { TestComponents.HydrogenGenerator }).Should().BeTrue();
        // Then
        facility[1].IsEmpty.Should().BeFalse();
        facility[1].Payload.Should().BeEquivalentTo(new[] { TestComponents.HydrogenChip });
        facility.Elevator.IsEmpty.Should().BeFalse();
        facility.Elevator.Payload.Should().BeEquivalentTo(new[] { TestComponents.HydrogenGenerator });
    }

    [Fact]
    public void ElevatorIsEmptyAfterAllComponentsAreLoadedIntoElevator()
    {
        // Given
        var floors = new Dictionary<int, Floor>{
            {1, new Floor(TestComponents.HydrogenGeneratorAndChip)},
            {2, new Floor()},
            {3, new Floor()},
            {4, new Floor()},
        };
        var facility = new Facility(floors);
        facility.LoadElevator(TestComponents.HydrogenGeneratorAndChip).Should().BeTrue();
        // When
        facility[1].IsEmpty.Should().BeTrue();
        // Then
    }


    [Theory]
    [ClassData(typeof(SecurityRulesCaseGenerator))]
    public void CheckSecurityRules(RtgComponent[] firstFloorPayload,
        RtgComponent[] secondFloorPayload,
        RtgComponent[] elevatorLoad,
        int floorShit,
        int expectedFloor,
        string testCaseName)
    {
        // Given
        var floors = new Dictionary<int, Floor>{
            {1, new Floor(firstFloorPayload)},
            {2, new Floor(secondFloorPayload)},
            {3, new Floor()},
            {4, new Floor()},
        };
        Console.WriteLine($"CheckSecurityRules() Running => {testCaseName}");

        var facility = new Facility(floors);
        facility.LoadElevator(elevatorLoad).Should().BeTrue(testCaseName);
        // When
        // Then
        facility.MoveElevator(floorShit).Should().Be(expectedFloor, testCaseName);
    }

    [Fact]
    public void WhenThereIsNoPossibleStateChange()
    {
        /*
         *  In this case, there is no possible movement.
         *  F4 .  .  .  .  .  
         *  F3 .  .  .  .  .  
         *  F2 .  HG .  .  .  
         *  F1 E  .  .  .  LM
         */
        // Given
        var floors = new Dictionary<int, Floor>
            {
                {1, new Floor(new[]{TestComponents.LithiumChip})},
                {2, new Floor(new[]{TestComponents.HydrogenGenerator})},
                {3, new Floor()},
                {4, new Floor()},
            };
        var facility = new Facility(floors);
        // When
        var possibleActions = facility.GetPossibleActions();
        // Then
        possibleActions.Should().BeEmpty();
    }

    [Theory(Skip = "WIP")]
    [ClassData(typeof(FacilityStatesCases))]
    public void GetPossibleLoadsAndMovesFromAGivenState(Dictionary<int, Floor> initialFloorState,
        RtgComponent[] expectedLoad,
        int expectedMove)
    {
        // Given
        var facility = new Facility(initialFloorState);
        // When
        var possibleActions = facility.GetPossibleActions();
        // Then


    }
}

#region  Auxiliary classes
public static class TestComponents
{
    public static RtgComponent HydrogenGenerator = new RtgComponent("generator", "hydrogen");
    public static RtgComponent HydrogenChip = new RtgComponent("microchip", "hydrogen");
    public static RtgComponent LithiumChip = new RtgComponent("microchip", "lithium");
    public static RtgComponent LithiumGenerator = new RtgComponent("generator", "lithium");
    public static RtgComponent[] HydrogenGeneratorAndChip = new[] { HydrogenGenerator, HydrogenChip };
    public static RtgComponent[] LithiumGeneratorAndChip = new[] { LithiumGenerator, LithiumChip };
}

public class SecurityRulesCaseGenerator : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        /*
         *  Elevator cannot move the hydrogen generator to the second floor because it'll
         *  fry the lithium chip.
         */
        new object[]
        {
            new[] {TestComponents.HydrogenGenerator},
            new[] {TestComponents.LithiumChip},
            new[] {TestComponents.HydrogenGenerator},
            1,
            1,
            "Elevator cannot risk a chip in the target floor [Case 1]"
        },

        /*
         *  Elevator can move the hydrogen generator to the second floor because 
         *  the lithium chip will be connected to its own generator.
         */
        new object[]
        {
            TestComponents.HydrogenGeneratorAndChip,
            TestComponents.LithiumGeneratorAndChip,
            new[] {TestComponents.HydrogenGenerator},
            1,
            2,
            "Elevator should move to the target floor becasue there is a chip connected to its own generator [Case 2]"
        },

        /*
         *  Elevator can move the hydrogen generator to the second floor because 
         *  there is no chip in that floor.
         */
        new object[]
        {
            new[] {TestComponents.HydrogenGenerator},
            new[] {TestComponents.LithiumGenerator},
            new[] {TestComponents.HydrogenGenerator},
            1,
            2,
            "Elevator should move to the target floor becasue there is no chip there [Case 3]"
        },

        /*
         *  Elevator can move the hydrogen generator to the second floor because 
         *  it is empty.
         */
        new object[]
        {
            TestComponents.HydrogenGeneratorAndChip,
            new RtgComponent[] { },
            new[] {TestComponents.HydrogenGenerator},
            1,
            2,
            "Elevator should move to the target floor becasue it is empty [Case 4]"
        },

        /*
         *  Elevator can move the hydrogen chip to the second floor because 
         *  it is and hydrogen generator there.
         */
        new object[]
        {
            new[] {TestComponents.HydrogenChip},
            new[] {TestComponents.HydrogenGenerator},
            new[] {TestComponents.HydrogenChip},
            1,
            2,
            "Elevator should move the chip to the target floor becasue there is a generator of the same material [Case 5]"
        },
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class FacilityStatesCases : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new List<object[]>
    {
        /*
         *  In this case, only possible action is load Hydrgen Generator into
         *  elevator and go up.
         *  F4 .  .  .  .  .  
         *  F3 .  .  .  .  .  
         *  F2 .  HG .  .  .  
         *  F1 E  .  HM .  LM
         */
        new object[]
        {
            new Dictionary<int, Floor>
            {
                {1, new Floor(new[]{TestComponents.HydrogenChip, TestComponents.LithiumChip})},
                {2, new Floor(new[]{TestComponents.HydrogenGenerator})},
                {3, new Floor()},
                {4, new Floor()},
            },
            new PossibleAction[]
            {
                new PossibleAction(new[]{TestComponents.HydrogenChip}, (int)MoveElevator.Up)
            },
            "There is a single possible action: load hydrogen chip and move it to the next floor"
        }
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}


#endregion Auxiliary classes