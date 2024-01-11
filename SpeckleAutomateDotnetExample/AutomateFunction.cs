using Objects;
using Objects.Geometry;
using Speckle.Automate.Sdk;
using Speckle.Core.Logging;
using Speckle.Core.Models.Extensions;
using Speckle.Newtonsoft.Json;

static class AutomateFunction
{
  public static async Task Run(
    AutomationContext automationContext,
    FunctionInputs functionInputs
  )
  {
    Console.WriteLine("Starting execution");
    _ = typeof(ObjectsKit).Assembly; // INFO: Force objects kit to initialize

    Console.WriteLine("Receiving version");
    var commitObject = await automationContext.ReceiveVersion();

    Console.WriteLine("Received version: " + commitObject);

    var count = commitObject
      .Flatten()
      .Count(b => b.speckle_type == functionInputs.SpeckleTypeToCount);

    Console.WriteLine($"Serialized -> {JsonConvert.SerializeObject(functionInputs.SpeckleTypeToCount)}");
    Console.WriteLine($"Number of speckle type to count {functionInputs.SpeckleTypeToCount}");
    Console.WriteLine($"Counted {count} objects");
    automationContext.MarkRunSuccess($"Counted {count} objects");
  }
}
