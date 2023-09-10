<script>
import {onMounted, ref} from 'vue'
import axios from "axios"
import router from '../../../router/router'
export default{
    setup(){
        
        let schedule = ref([]);

        let gotoAllHistory = ()=>{
            router.push({path:"allHistory"})
        }
        onMounted(()=>{
            axios.post(import.meta.env.VITE_API_URL + "/PostBetHistory"
            ,{UserId:localStorage.getItem("userId")}
            ,{
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`
                }
            })
            .then(res=>{
                console.log(res)
                res.data.forEach(element => {
                    schedule.value.push(element);
                });
                console.log(schedule.value)
            })
            .catch(err=>{
                console.log(err)
                router.push({path:"/signin"})
            })
        })
        return { 
            schedule,
            gotoAllHistory
        };
    }
}
</script>

<template>
    <div class="sm-Header_Nav">
        <div class="sph-LiveInPlayHeader">
            <div class="sph-LiveInPlayHeader_Scroller">
                <div class="sph-HorizontalLiveNavBarScroller_Wrapper">
                    <div class="sph-HorizontalLiveNavBarScroller_Contents">
                        <div class="sph-LiveInPlayButtonBarButton" v-on:click="gotoAllHistory">所有</div>
                        <div class="sph-LiveInPlayButtonBarButton">未結算</div>
                        <div class="sph-LiveInPlayButtonBarButton">已結算</div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="gl-MarketGroup_Wrapper">
        <div class="gl-MarketGroupContainer">
            <table class="sgl-MarketFixtureDetailsLabel-table">
                <tr style="background-color: #222;" class="sgl-MarketFixtureDetailsLabel-table-header">
                    <th>日期</th>
                    <th>時間</th>
                    <th>隊伍</th>
                    <th>賠率</th>
                    <th>押注金額</th>
                    <th>押注隊伍</th>
                    <!-- <th>結算狀態</th> -->
                </tr>
                <tr v-for="(item, index) in schedule" class="sgl-MarketFixtureDetailsLabel-table-content" v-show="item.closeEvent">
                    <td><div>{{ item.dateTime.split('T')[0] }}</div></td>
                    <td>{{ item.dateTime.split('T')[1].split('Z')[0] }}</td>
                    <td style="display: flex;">
                        <span v-on:click="selectTeam(index, item.awayTeamName, item.awayTeamOdds)" style="width: 35%;" class="gl-MarketFixtureDetailsLabel-table-content-away"><a href="#creditCard" style="color: #fff;">{{ item.awayTeamName }}</a> </span>
                        <span style="width: 5%;">{{ item.awayTeamPoint }}</span>
                        <span style="width: 20%;">vs</span> 
                        <span style="width: 5%;">{{ item.homeTeamPoint }}</span>
                        <span v-on:click="selectTeam(index, item.homeTeamName, item.homeTeamOdds)" class="gl-MarketFixtureDetailsLabel-table-content-home" style="width: 35%;"><a href="#creditCard" style="color: #fff;">{{ item.homeTeamName }}</a></span></td>
                    <td>{{ item.awayTeamOdds }} : {{ item.homeTeamOdds }}</td>
                    <td>{{ item.money }}</td>
                    <td>{{ item.betTeamName }}</td>
                    <!-- <td></td> -->
                </tr>
            </table>
        </div>
    </div>
</template>

<style scoped>
.sph-LiveInPlayHeader{
    background-color: transparent;
    color: #c3c3c3;
    height: 43px;
    position: relative;
    display: flex;
    /* padding-bottom: 2px; */
    /* border-bottom: 2px solid #367a65; */
}

.sph-LiveInPlayHeader_Scroller{
    flex-grow: 1;
    min-width: 0;
    position: relative;
}

.sph-HorizontalLiveNavBarScroller_Wrapper{
    overflow-x: hidden;
    padding-bottom: 2px;
    z-index: 10;
}

.sph-HorizontalLiveNavBarScroller_Contents {
    display: flex;
    position: relative;
    white-space: nowrap;
}

.sph-LiveInPlayButtonBarButton {
    padding: 0 13px 0 12px;
    color: #c3c3c3;
    font-size: 13px;
    display: flex;
    flex-shrink: 0;
    height: 45px;
    line-height: 45px;
    cursor: pointer;
    position: relative;
}

.gl-MarketGroup_Wrapper{
    background-color: #383838;
}

.gl-MarketGroupContainer {
    display: flex;
    flex-wrap: wrap;
    width: 100%;
    font-size: 0;
}

.sgl-MarketFixtureDetailsLabel{
    width: 50%;
}

.sgl-MarketOddsExpand{
    width: 50%;
}

.rcl-MarketHeaderLabel{
    width: 100%;
    background-color: #383838;
    min-height: 30px;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
    color: #fff;
    font-size: 12px;
    font-weight: 700;
    line-height: 30px;
}

.rcl-MarketColumnHeader {
    width: 100%;
    background-color: #383838;
    min-height: 30px;
    text-align: center;
    padding: 0 5px;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
    color: #fff;
    font-size: 12px;
    font-weight: 700;
    line-height: 30px;
}

.sac-ParticipantCenteredStacked50OTB{

}

.sgl-MarketFixtureDetailsLabel-table{
    width: 100%;
    background-color: #383838;
    min-height: 30px;
    text-align: center;
    padding: 0 5px;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
    color: #fff;
    font-size: 12px;
    font-weight: 700;
    line-height: 30px;
}

.sgl-MarketFixtureDetailsLabel-table-header{
    background-color: #222;
}

.sgl-MarketFixtureDetailsLabel-table > tr:nth-child(odd){
    background-color: #444;
}

.sgl-MarketFixtureDetailsLabel-table > tr:nth-child(odd):hover{
    background-color: #222;
}

.sgl-MarketFixtureDetailsLabel-table > tr:nth-child(even){
    background-color: #555;
}

.sgl-MarketFixtureDetailsLabel-table > tr:nth-child(even):hover{
    background-color: #222;
}

.gl-MarketFixtureDetailsLabel-table-content-away{
    cursor: pointer;
}

.gl-MarketFixtureDetailsLabel-table-content-home{
    cursor: pointer;
}

.gl-MarketFixtureDetailsLabel-table-content-away:hover{
    color: #00FFB4;
}

.gl-MarketFixtureDetailsLabel-table-content-home:hover{
    color: #00FFB4;
}

tr{
    cursor: default;
}
</style>