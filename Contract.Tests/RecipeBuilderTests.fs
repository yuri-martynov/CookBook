namespace Contract.Tests
open System
open NUnit.Framework

open Types
open RecipeBuilder

[<TestFixture>]
type RecipeBuilderTests() = 

    [<Test>]
    member x.BuildsRecipe() =

        let dish = recipe {
        
            блюдо   "мороженка"
            
            шаг     "вскипятить воду"
            //время   10 минут
            //состав  "вода" 5 литров
            //состав  "соль" 6 столовых_ложек              
            шаг     "заморозить воду"
            //время   2 часа
            
            шаг     "растопить шоколад"
            //время   1 минута
            //состав  "шоколадка" 1 шт

            шаг     "полить шоколадом"
            //время   1 минута
        }
        
        Assert.AreEqual("мороженка", dish.name)
        Assert.AreEqual(4, dish.recipe.steps |> Seq.length)
        
        //let step = dish.recipe.steps |> Seq.head 
        //Assert.AreEqual("вскипятить воду", step.description)
        //Assert.AreEqual(TimeSpan.FromMinutes 10.0, step.duration)
        //Assert.AreEqual(2, step.ingredients |> Seq.length)
        
        //let ingredient = step.ingredients |> Seq.head
        //Assert.AreEqual(Only {product = Whole "вода"; quantity = Liters 5.0}, ingredient )