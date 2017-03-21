namespace Scenario.Functional.Core.ViewModels

open Caliburn.Micro

type ShellViewModel() = 
    inherit Screen()

    let mutable name = "Enter your name"

    override this.OnInitialize () =
        this.DisplayName <- "Shell"

    member this.Name
        with get() = name
        and set(value) =
            name <- value
            base.NotifyOfPropertyChange()

    member this.SayHello () =
        this.DisplayName <- "Hello " + this.Name