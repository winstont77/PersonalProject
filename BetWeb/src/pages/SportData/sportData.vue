<script>
import {onMounted, ref} from 'vue'
import axios from "axios"
export default{
    setup(){
        let events = ref([])
        onMounted(()=>{
            (async()=>{
                // await axios.get("https://localhost:7099/GetSoccer")
                // .then(res=>{
                //     console.log(res)
                //     res.data.forEach(element => {
                //         events.push({
                //             AwayTeamName:element.away_team,
                //             HomeTeamName:element.home_team,
                //             AwayTeamOdds:element.bookmakers[0].markets[0].outcomes[0].price,
                //             HomeTeamOdds:element.bookmakers[0].markets[0].outcomes[1].price,
                //             Sports:"soccer",
                //             DateTime:element.commence_time
                //         })
                //     });
                //     console.log(events)
                // })
                // .catch(err=>{
                //     console.log(err)
                // })

                // await axios.get("https://localhost:7099/GetFootball")
                // .then(res=>{
                //     console.log(res)
                //     res.data.forEach(element => {
                //         events.push({
                //             AwayTeamName:element.away_team,
                //             HomeTeamName:element.home_team,
                //             AwayTeamOdds:element.bookmakers[0].markets[0].outcomes[0].price,
                //             HomeTeamOdds:element.bookmakers[0].markets[0].outcomes[1].price,
                //             Sports:"football",
                //             DateTime:element.commence_time
                //         })
                //     });
                //     console.log(events)
                // })
                // .catch(err=>{
                //     console.log(err)
                // })

                // await axios.get("https://localhost:7099/GetBaseball")
                // .then(res=>{
                //     console.log(res)
                //     res.data.forEach(element => {
                //         events.push({
                //             AwayTeamName:element.away_team,
                //             HomeTeamName:element.home_team,
                //             AwayTeamOdds:element.bookmakers[0].markets[0].outcomes[0].price,
                //             HomeTeamOdds:element.bookmakers[0].markets[0].outcomes[1].price,
                //             Sports:"baseball",
                //             DateTime:element.commence_time
                //         })
                //     });
                //     console.log(events)
                // })
                // .catch(err=>{
                //     console.log(err)
                // })

                await axios.get(import.meta.env.VITE_API_URL + "/GetBasketball")
                .then(res=>{
                    console.log(res)
                    res.data.forEach(element => {
                        events.value.push({
                            AwayTeamName:element.away_team,
                            HomeTeamName:element.home_team,
                            AwayTeamOdds:element.bookmakers[0].markets[1].outcomes[0].price,
                            HomeTeamOdds:element.bookmakers[0].markets[1].outcomes[1].price,
                            AwayTeamPoint:parseFloat(element.bookmakers[0].markets[1].outcomes[0].point),
                            HomeTeamPoint:parseFloat(element.bookmakers[0].markets[1].outcomes[1].point),
                            Sports:"basketball",
                            DateTime:element.commence_time
                        })
                        console.log(typeof element.bookmakers[0].markets[1].outcomes[0].point)
                    });
                    console.log(events.value)
                })
                .catch(err=>{
                    console.log(err)
                })

                // axios.post("https://localhost:7099/PostSports",[
                //     {Name:"soccer"},
                //     {Name:"football"},
                //     {Name:"baseball"},
                //     {Name:"basketball"}
                // ])
                // .then(res=>{
                //     console.log(res)
                // })
                // .catch(err=>{
                //     console.log(err)
                // })

                await axios.post("https://localhost:7109/PostEvents",events.value)
                .then(res=>{
                    console.log(res)
                })
                .catch(err=>{
                    console.log(err)
                })
            })()

            // axios.post("https://localhost:7099/PostSports",[
            //     {Name:"soccer"},
            //     {Name:"football"},
            //     {Name:"baseball"},
            //     {Name:"basketball"}
            // ])
            // .then(res=>{
            //     console.log(res)
            // })
            // .catch(err=>{
            //     console.log(err)
            // })
        })
    }
}
</script>

<template>
    sportdata
</template>

<style scoped>

</style>