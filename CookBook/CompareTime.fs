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

    let times = dishesWithTime |> Seq.map (snd >> (fun t -> t.TotalMinutes))
    let avg = times |> Seq.average
    let div = times |> Seq.map (fun t -> Math.Abs(t-avg)) |> Seq.max
    
    if div < avg * 0.2 then
        return "примерно одинакого в среднем: " + ((TimeSpan.FromMinutes avg) |> Format.duration)
    else
        let format ((d,t) : (Dish*TimeSpan)) =
            d.name + " - " + (Format.duration t)

        return 
            dishesWithTime
            |> Seq.sortBy snd
            |> Seq.map format
            |> Format.list ". "

}

