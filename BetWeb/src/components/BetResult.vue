<script setup>
import axios from 'axios';
import { ref } from 'vue'

let prop = defineProps({
  odds:null,
  team:null,
  event:{}
})

let emit = defineEmits(['betResultEmit'])

let betResultButton = (display, sureOrCancel)=>{
    if(sureOrCancel===true){
        axios.post("https://localhost:7099/PostBets",{
            "Bet": {
                    "events": {
                    "id": prop.event.id,
                    "awayTeamName": prop.event.awayTeamName,
                    "homeTeamName": prop.event.homeTeamName,
                    "awayTeamOdds": prop.event.awayTeamOdds,
                    "homeTeamOdds": prop.event.homeTeamOdds,
                    "sports": prop.event.sports,
                    "dateTime": prop.event.dateTime
                },
                "dateTime": getCurrentDateInFormat().toString(),
                "money": 0
            },
            "UserId":1
        },{
        headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`
        }})
        .then(res=>{
            console.log(res)
        }).catch(err=>[
            console.log(err)
        ])
    }else{
        console.log("cancel bet")
    }


    
    // console.log({Money:betMoney.value, 
    //         DateTIme:getCurrentDateInFormat(), 
    //         Event:{
    //             Id:prop.event.id,
    //             Sports:prop.event.sports,
    //             DateTIme:prop.event.dateTime,
    //             AwayTeamName:prop.event.awayTeamName,
    //             AwayTeamOdds:prop.event.awayTeamOdds,
    //             HomeTeamName:prop.event.homeTeamName,
    //             HomeTeamOdds:prop.event.homeTeamOdds
    //         }})
    emit("betResultEmit", display)
}

let getCurrentDateInFormat = () => {
    const now = new Date();

    const year = now.getFullYear();
    const month = String(now.getMonth() + 1).padStart(2, '0');  // Month is 0-indexed, so we add 1
    const day = String(now.getDate()).padStart(2, '0');
    const hour = String(now.getHours()).padStart(2, '0');
    const seconds = String(now.getSeconds()).padStart(2, '0');

    return `${year}-${month}-${day}T${hour}:${seconds}:00Z`;
}

let updateBetMoney = (event) => {
    betMoney.value = event.target.value;
}

let betMoney = ref(1);

</script>

<template>
  <div class="betResult">
    <div class="betResult-content">
        <div class="betResult-content-table">
            <table>
                <tr>
                    <th>投注金額</th>
                    <th>賠率</th>
                    <th style="width:60%">投注隊伍</th>
                </tr>
                <tr>
                    <td>{{betMoney}}</td>
                    <td>{{ odds }}</td>
                    <td>{{ team }}</td>
                </tr>
            </table>
        </div>
        <div style="display: flex;">
            <input type="range" class="betmoneyinputrange" min="100" max="500" value="50" v-on:input="updateBetMoney">
        </div>
        <div class="betResult-content-surebutton">
            <div class="betResult-content-surebutton-content">
                <div class="betResult-content-surebutton-text" v-on:click="betResultButton(false, true)">確認</div>
                <div class="betResult-content-surebutton-textcancel" v-on:click="betResultButton(false, false)">取消</div>
            </div>
        </div>
    </div>
  </div>
</template>

<style scoped>
.betResult {
  border-top: 2px solid #367a65;
  background-color: #383838;
  position: absolute;
  z-index: 100;
  color: black;
  top: 0;
  width: 100%;
  height: 100%;
}

.betResult-content{
    /* background-image: linear-gradient(rgba(12,22,20,.1),transparent 20px),radial-gradient(122% 370px at center -220px,#009969 0,transparent 100%),linear-gradient(to right bottom,#0c1614,#084436); */
}

.betResult-content-table{
    margin-top: 30px;
    width: 100%;
}

.betResult-content-table > table{
    width: 90%;
    margin: auto;
}

.betResult-content-table  table tr th{
    width: 25%;
    text-align: center;
    color: #DDDDDD;
    background-color: #222;
    padding: 5px 0;
}

.betResult-content-table  table tr td{
    text-align: center;
    color: #DDDDDD;
    padding: 5px 0;
}


.betResult-content-surebutton{  
    margin-top: 30px;
    color: #fff;
    display: flex;
}

.betResult-content-surebutton-content{
    margin: auto;
    display: flex;
}

.betResult-content-surebutton-text{
    background-color: #126e51;
    padding: 5px 10px;
    border-radius: 3px;
    cursor: pointer;
}

.betResult-content-surebutton-textcancel{
    padding: 5px 10px;
    cursor: pointer;
    margin-left: 20px;
}



.betmoneyinputrange {
  -webkit-appearance: none;
  appearance: none;
  background: transparent;
  cursor: pointer;
  width: 88%;
  margin: auto;
  margin-top: 30px;
}

.betmoneyinputrange::-webkit-slider-thumb{
    -webkit-appearance: none;
    height: 10px;
    width: 10px;
    border-radius: 50%;
    background-color: #fff;
}

.betmoneyinputrange::-webkit-slider-runnable-track {
    /* background-color: #126e51; */
  background: #367a65;
  height: 0.5rem;
  border-radius: 5px;
}
</style>
