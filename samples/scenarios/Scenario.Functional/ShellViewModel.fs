namespace Scenario.Functional.Core.ViewModels

open Caliburn.Micro
open System.Threading.Tasks

type ShellViewModel() = 
    inherit Screen()

    let mutable name = "Enter your name"

    override this.OnInitializedAsync (cancellationToken) =
        async {
            this.DisplayName <- "Shell"
        }
        |> Async.StartAsTask :> Task

    member this.Name
        with get() = name
        and set(value) =
            name <- value
            base.NotifyOfPropertyChange()

    member this.SayHello () =
        this.DisplayName <- "Hello " + this.Name
