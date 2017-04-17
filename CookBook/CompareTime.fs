module CompareTime

open Types
open System

let get (getDishById: getDishById) (dishIds: seq<string>) : Async<string> = async {

    let getIdAndTime id = async {
        let! dish = getDishById id
        let t = Time.get' dish
        return (dish, t)
    }

    let! dishesWithTime = 
        dishIds 
        |> Seq.map getIdAndTime
        |> Async.Parallel

    let format ((d,t) : (Dish*TimeSpan)) =
        d.name + " - " + (Format.duration t)

    return 
        dishesWithTime
        |> Seq.sortBy snd
        |> Seq.map format
        |> Format.list ". "

}

