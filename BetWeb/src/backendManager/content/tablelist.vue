<script>
import { onMounted, ref } from 'vue';
import { reactive } from 'vue';
import axios from 'axios'
import router from "../../router/router"

export default{
    setup() {
        let displaySelectTeamWin = ref(false)
        let displayAwayTeam = ref("")
        let displayHomeTeam = ref("")
        let selectTeamToWin = ref("")
        let selectTeamToLose = ref("")
        let awayTeamScore = ref(null)
        let homeTeamScore = ref(null)
        let eventID = ref(null)
        let schedule = ref([])
        let selectEvent = (index)=>{
            displaySelectTeamWin.value = true

            displayAwayTeam.value = schedule.value[index].awayTeamName
            displayHomeTeam.value = schedule.value[index].homeTeamName

            eventID.value = index 

            // console.log(schedule.value[index])
            // axios.post("https://localhost:7099/PostCloseEvent",{id:schedule.value[index].id})
            // .then(res=>{
            //     console.log(res)
            // })
            // .catch(err=>{
            //     console.log(err)
            // })
        }

        let selectTeamWindowButton = (val)=>{
            if(val===true){
                // console.log(schedule.value[index])
                // axios.post("https://localhost:7099/PostCloseEvent",{id:schedule.value[index].id})
                // .then(res=>{
                //     console.log(res)
                // })
                // .catch(err=>{
                //     console.log(err)
                // })
                if(awayTeamScore.value>homeTeamScore.value){
                    selectTeamToWin.value = displayAwayTeam.value
                    selectTeamToLose.value = displayHomeTeam.value
                    // console.log(displayAwayTeam.value)
                }else{
                    selectTeamToWin.value = displayHomeTeam.value
                    selectTeamToLose.value = displayAwayTeam.value
                    // console.log(displayHomeTeam.value)
                }

                console.log(awayTeamScore.value)
                console.log(homeTeamScore.value)
                console.log(schedule.value[eventID.value].id)
                console.log(selectTeamToWin.value)
                console.log(selectTeamToLose.value)

                axios.post("https://localhost:7099/PostCloseEvent",
                {
                    Id:schedule.value[eventID.value].id,
                    WinTeamName: selectTeamToWin.value,
                    LoseTeamName: selectTeamToLose.value,
                    AwayTeamScore:awayTeamScore.value,
                    HomeTeamScore:homeTeamScore.value,
                    Events:schedule.value[eventID.value]
                })
                .then(res=>{
                    console.log(res)
                })
                .catch(err=>{
                    console.log(err)
                })
            }
            displaySelectTeamWin.value = false;
        }

        
        onMounted(()=>{
            axios.get("https://localhost:7099/GetEvents")
                .then(res => {
                console.log(res);
                res.data.forEach(element => {
                    if (element.sports === "basketball") {
                        schedule.value.push(element);
                    }
                });
                console.log(schedule.value);
            })
                .catch(err => {
                console.log(err);
            });
        })
        return {schedule, displaySelectTeamWin, displayAwayTeam, displayHomeTeam, selectTeamToWin, awayTeamScore, homeTeamScore, selectEvent, selectTeamWindowButton}
    },
    components: {  },
}
</script>

<template>
    <div style="width: 100%; display: flex; position: relative;">
        <table class="" style="width: 95%; margin: auto;border-collapse: collapse;margin-top: 30px;">
            <tr class="backendmanager-table-header" style="cursor: default;">
                <th class="backendmanager-table-header-th">比賽日期</th>
                <th class="backendmanager-table-header-th">比賽時間</th>
                <th class="backendmanager-table-header-th" style="width: 50%;">比賽隊伍</th>
                <th class="backendmanager-table-header-th">比賽賠率</th>
                <th class="backendmanager-table-header-th">比賽狀態</th>
                <th class="backendmanager-table-header-th"></th>
            </tr>
            <tr class="backendmanager-table-tr" v-for="(item, index) in schedule" style="border-bottom: 2px solid #126e51;cursor: default;">
                <td class="backendmanager-table-td">{{ item.dateTime.split('T')[0] }}</td>
                <td class="backendmanager-table-td">{{ item.dateTime.split('T')[1].split('Z')[0] }}</td>
                <td style="display: flex;height: 100%;" class="backendmanager-table-td">
                    <div style="width: 40%;display: flex;height: 100%;padding-top: 9px;" class=""><div style="margin: auto;">{{ item.awayTeamName }}</div></div>
                    <div style="width: 20%;margin: auto;padding-top: 9px;">vs</div> 
                    <div class="" style="width: 40%;margin: auto;padding-top: 9px;">{{ item.homeTeamName }}</div>
                </td>
                <td class="backendmanager-table-td">{{ item.awayTeamOdds }} : {{ item.homeTeamOdds }}</td>
                <td class="backendmanager-table-td">{{ item.closeEvent }}</td>
                <td class="backendmanager-table-td">
                    <div class="backendmanager-table-td-controll">
                        <div class="backendmanager-table-td-controll-button" v-on:click="selectEvent(index)">收盤</div>
                    </div>
                </td>
            </tr>
        </table>
        <div v-show="displaySelectTeamWin" class="selectTeamWinBG">
        </div>
        <div class="selectTeamWinWindow" v-show="displaySelectTeamWin">
            <div class="selectTeamWinWindow-header">
                設定隊伍比分
            </div>
            <div class="selectTeamWinWindow-content">
                <div style="display: flex;">
                    <input type="radio" name="choice" v-bind:value="displayAwayTeam" v-model="selectTeamToWin">
                    <div style="width: 30%;">{{ displayAwayTeam }}</div>
                    <input style="width: 30%; border: 1px solid #ddd;" type="number" name="" id="" v-model="awayTeamScore">
                </div>
                <div style="display: flex;">
                    <input type="radio" name="choice" v-bind:value="displayHomeTeam" v-model="selectTeamToWin">
                    <div style="width: 30%;">{{ displayHomeTeam }}</div>
                    <input style="width: 30%; border: 1px solid #ddd;" type="number" name="" id="" v-model="homeTeamScore">
                </div>
            </div>
            <div class="selectTeamWinWindow-button">
                <div class="selectTeamWinWindow-button-sure" v-on:click="selectTeamWindowButton(true)">確認</div>
                <div style="width: 20px;"></div>
                <div class="selectTeamWinWindow-button-cancel" v-on:click="selectTeamWindowButton(false)">取消</div>
            </div>
        </div>
    </div>
</template>

<style>
.backendmanager-table-header-th{
    width: 10%;
    color: #00000099;
    background-color: #DDD;
    /* color: #ffffff; */
    /* background-color: #52917e; */
    padding: 3px;
    border-bottom: 2px solid #126e51;cursor: default;
    border-top: 2px solid #126e51;cursor: default;
}

.backendmanager-table-td{
    text-align: center;
}

.backendmanager-table-tr:hover{
    background-color: #DDD;
    color: #ffffff;
}

.backendmanager-table-td-controll{
    display: flex;
    margin: 5px 0px;
}

.backendmanager-table-td-controll-button{
    margin: auto;
    width: 50px;
    height: 30px;
    background:linear-gradient(90deg,#337761,#52917e);
    border-radius: 5px;
    color: #fff;
    cursor: pointer;
    padding-top: 4px;
}

.backendmanager-table-td-controll-button:hover{
    background:linear-gradient(90deg,#4bac8d,#60ac95);
}

.selectTeamWinBG{
    width: 100%;
    height: 100%;
    position: absolute;
    background-color: #fff;
    opacity: .44;
}

.selectTeamWinWindow{
    width: 450px;
    height: 250px;
    background-color: #fff;
    position: fixed;
    top: 50%;           /* 移動到螢幕的垂直中央 */
    left: 55%;          /* 移動到螢幕的水平中央 */
    transform: translate(-50%, -50%); 
    border-radius: 5px;
    box-shadow: 5px 5px 10px gray;
}

.selectTeamWinWindow-header{
    height: 30px;
    background-color: #126e51;
    border-top-left-radius: 5px;
    border-top-right-radius: 5px;
    text-align: center;
    padding-top: 5px;
    color: #fff;
}

.selectTeamWinWindow-content{
    height: calc(100% - 70px);
}

.selectTeamWinWindow-button{
    margin-top: auto;
    display: flex;
    justify-content: center;
}

.selectTeamWinWindow-button-sure{
    padding: 5px 12px;
    border-radius: 5px;
    background-color: #126e51;
    color: #fff;
}

.selectTeamWinWindow-button-cancel{
    padding: 3px 10px;
    border: #126e51 solid 2px;
    border-radius: 5px;
    color: #126e51;
}
</style>