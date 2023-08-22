<script>
import { onMounted, ref } from 'vue';
import { reactive } from 'vue';
import axios from 'axios'
import router from "../../router/router.js"

export default{
    setup() {
        let betHistory = ref([]);
        onMounted(()=>{
            axios.post("https://localhost:7099/PostMemberDetail",{
                Name:localStorage.getItem("userName")
            },
            {
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`
                }
            })
            .then(res=>{
                console.log(res)
                if(res.data.bet.length>0){
                    console.log(res)
                    res.data.bet.forEach(element => {
                        betHistory.value.push(element)
                    });
                }
            })
            .catch(err=>{
                console.log(err)
                router.push({path:"/signin"})
            })
        })
        return {betHistory}
    },
    components: {  }
}
</script>

<template>
    <div class="wc-PageView">
        <div class="wc-PageView_Main">
            <div class="wcl-CommonElementStyle_NavContentContainer">
                <div class="myb-MyBetsModule">
                    <div class="myb-MyBets">
                        <div class="myb-MyBetsHeader">
                            <div class="myb-MyBetsHeader_Scroller">
                                <div class="myb-HeaderButton">未結算</div>
                                <div class="myb-HeaderButton">已結算</div>
                                <div class="myb-HeaderButton">全部</div>
                            </div>
                        </div>
                        <div class="myb-MyBetsScroller">
                            <div>
                                <div class="myb-MyBets_Container">
                                    <div class="myb-BetItemsContainer_EmptyMessage">
                                        <!-- <div class="myb-BetItemsContainer_NoBetsMessageLineOne">
                                            當前無投注可顯示
                                        </div> -->
                                        <table class="sgl-MarketFixtureDetailsLabel-table">
                                            <tr style="background-color: #222;" class="sgl-MarketFixtureDetailsLabel-table-header">
                                                <th>押注日期</th>
                                                <th>押注時間</th>
                                                <th>押注隊伍</th>
                                                <th>押注賠率</th>
                                                <th>比賽隊伍</th>
                                                <th>比賽賠率</th>
                                            </tr>
                                            <tr v-for="(item, index) in betHistory" class="sgl-MarketFixtureDetailsLabel-table-content">
                                                <td><div>{{ item.dateTime.split('T')[0] }}</div></td>
                                                <td>{{ item.dateTime.split('T')[1].split('Z')[0] }}</td>
                                                <td>{{ item.betTeamName }}</td>
                                                <td>{{ item.betTeamOdds }}</td>
                                                <td style="display: flex;">
                                                    <span v-on:click="selectTeam(index, item.awayTeamName, item.awayTeamOdds)" style="width: 40%;" class="gl-MarketFixtureDetailsLabel-table-content-away">{{ item.awayTeamName }} </span>
                                                    <span style="width: 20%;">vs</span> 
                                                    <span v-on:click="selectTeam(index, item.homeTeamName, item.homeTeamOdds)" class="gl-MarketFixtureDetailsLabel-table-content-home" style="width: 40%;">{{ item.homeTeamName }}</span></td>
                                                <td>{{ item.awayTeamOdds }} : {{ item.homeTeamOdds }}</td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style>
.wc-PageView{
    display: flex;
    align-items: stretch;
    margin: 0 auto;
    max-width: 1450px;
    height: calc(100vh - 105px);
}

.wc-PageView_Main{
    flex: 1 1 auto;
    width: auto;
    position: relative;
    display: flex;
    align-items: stretch;
    min-width: 0;
    height: 100%;
    background-color: #383838;
}

.wcl-CommonElementStyle_NavContentContainer {
    display: flex;
    align-items: stretch;
    width: 100%;
    height: 100%;
}

.myb-MyBetsModule{
    width: 100%;
    height: 100%;
    background-color: #282828;
}

.myb-MyBets{
    position: relative;
    display: flex;
    flex-direction: column;
    height: 100%;
}

.myb-MyBetsHeader{
    position: relative;
    width: 100%;
    display: flex;
    height: 50px;
    background-image: none;
    font-size: 0;
    z-index: 1;
}

.myb-MyBetsHeader_Scroller{
    overflow-x: hidden;
    margin: 0 20px;
    overflow-y: hidden;
    height: 50px;
    flex: 1 1 100%;
    white-space: nowrap;
    padding: 0;
    width: 100%;
    position: relative;
}

.myb-HeaderButton{
    color: #ddd;
    font-weight: 700;
    line-height: 50px;
    height: 50px;
    padding: 0 10px;
    display: inline-flex;
    align-items: center;
    cursor: pointer;
    flex-direction: column;
    position: relative;
    z-index: 1;
    min-width: 40px;
    font-size: 13px;
}

.myb-MyBetsScroller{
    overflow-y: auto;
    overflow-x: hidden;
    overscroll-behavior: none;
    height: calc(100% - 50px);
}

.yb-MyBets_Container{
    min-height: calc(100vh - 150px);
    margin: 0 30px 0 20px;
    backface-visibility: hidden;
    position: relative;
    transition: transform .5s ease;
}

.myb-BetItemsContainer_EmptyMessage {
    padding: 30px 15px;
    background: #353535;
    color: #ddd;
    text-align: center;
    position: relative;
    border-radius: 5px;
    margin: 0 20px;
}

.myb-BetItemsContainer_NoBetsMessageLineOne{
    font-weight: 700;
    font-size: 14px;
    margin-bottom: 10px;
    line-height: 18px;
}
</style>