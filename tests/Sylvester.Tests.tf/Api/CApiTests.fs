namespace Sylvester.Tests.tf.Api

module CApiTests =

    open System
    open Xunit
    open TensorFlow

    [<Fact>]
    let ``Can get version`` () =
        Assert.Equal("2.0.0", c_api.TF_Version())
