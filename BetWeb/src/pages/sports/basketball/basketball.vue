<script>
import { onMounted, ref } from 'vue';
import { reactive } from 'vue';
import axios from 'axios'
import BetResult from '../../../components/BetResult.vue';


export default{
    setup() {
        let schedule = ref([]);
        let displayBetResult = ref(false)
        let selectodds = ref(null)
        let selectteamname = ref("")
        let selectevent = ref({})
        let selectTeam = (index, awayOrHome, odds) => {
            console.log(index, awayOrHome, odds);
            selectodds.value = odds
            selectteamname.value = awayOrHome
            displayBetResult.value = true
            selectevent.value = schedule.value[index]
        };

        let handleBetResultEmit = (value) => {
            displayBetResult.value = value; 
        }
        onMounted(() => {
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
        });
        return { schedule, displayBetResult, selectodds, selectteamname, selectevent, selectTeam, handleBetResultEmit };
    },
    components: { BetResult }
}
</script>
<template>
    <div class="wcl-PageContainer_Colcontainer">
        <div class="wcl-PageContainer_Col1 ">
            <div class="wcl-CommonElementStyle_PrematchCenter">
                <div class="sm-Header">
                    <div class="sm-Header_Title">
                        <div class="sm-Header_TitleText ">籃球</div>
                    </div>
                    <div class="sm-Header_Nav ">
                        <div class="sph-LiveInPlayHeader">
                            <div class="sph-LiveInPlayHeader_Scroller">
                                <div class="sph-HorizontalLiveNavBarScroller_Wrapper">
                                    <div class="sph-HorizontalLiveNavBarScroller_Contents ">
                                        <div class="sph-LiveInPlayButtonBarButton">投注</div>
                                        <div></div>
                                        <div></div>
                                    </div>
                                </div>
                            </div>
                            <div></div>
                        </div>
                    </div>
                    <div class="sm-SplashModule ">
                        <div class="gl-MarketGrid">
                            <div class="ff-FeaturedFixtureLoader ">
                                <div>
                                    <div class="ff-Header ">
                                        <div class="ff-Header_Title ">精選賽事</div>
                                    </div>
                                </div>
                            </div>
                            <div class="gl-MarketGroup_Wrapper">
                                <div class="gl-MarketGroupContainer">
                                    <!-- <div class="sgl-MarketFixtureDetailsLabel">
                                        <div class="rcl-MarketHeaderLabel">賽程</div>
                                        <div class="rcl-MarketHeaderLabel" v-for="item in schedule">
                                            <div>{{ item.dateTime.split('T')[0] }}</div>
                                            <div>{{ item.awayTeamName }} </div>
                                            <div>{{ item.homeTeamName }}</div>
                                        </div>
                                    </div>
                                    <div class="sgl-MarketFixtureDetailsLabel">
                                        <div class="rcl-MarketHeaderLabel">賠率</div>
                                        <div class="rcl-MarketHeaderLabel" v-for="item in schedule">
                                            {{ item.awayTeamOdds }} : {{ item.homeTeamOdds }}
                                        </div>
                                    </div> -->
                                    <table class="sgl-MarketFixtureDetailsLabel-table">
                                        <tr style="background-color: #222;" class="sgl-MarketFixtureDetailsLabel-table-header">
                                            <th>日期</th>
                                            <th>時間</th>
                                            <th>隊伍</th>
                                            <th>賠率</th>
                                        </tr>
                                        <tr v-for="(item, index) in schedule" class="sgl-MarketFixtureDetailsLabel-table-content">
                                            <td><div>{{ item.dateTime.split('T')[0] }}</div></td>
                                            <td>{{ item.dateTime.split('T')[1].split('Z')[0] }}</td>
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
                        <BetResult v-show="displayBetResult" v-bind:odds=selectodds v-bind:team=selectteamname v-bind:event="selectevent" @betResultEmit="handleBetResultEmit"></BetResult>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style>
.wcl-PageContainer_Colcontainer{
    min-height: calc(100vh - 105px);
    display: flex;
    flex-direction: row;
    flex: 1 0 auto;
}

.wcl-PageContainer_Col1 {
    flex: 0 0 100%;
    vertical-align: top;
    max-width: 100%;
}

.wcl-CommonElementStyle_PrematchCenter{
    overflow: hidden;
    position: relative;
    min-width: 0;
}

.sm-Header{
    background-size: 1800px auto;
    width: 100%;
    background-color: #222;
    background-position: 100% 0;
    background-repeat: no-repeat;
    z-index: 10;
    padding-top: 40px;
    position: relative;
    background-image: url(../../../images/backgroundImage/18-Basketball-desktop.jpg);
}

.sm-Header_Title{
    padding: 3px 20px 10px;
    display: flex;
}

.sm-Header_TitleText{
    font-size: 24px;
    line-height: 32px;
    font-weight: 700;
    color: #ddd;
}

.sm-Header_Nav {

}

.sm-Header_Nav::after{
    content: "";
    position: absolute;
    left: 0;
    bottom: -2px;
    width: 100%;
    height: 2px;
    background-color: #367a65;
    z-index: 1;
}

.sph-LiveInPlayHeader{
    background-color: transparent;
    color: #c3c3c3;
    height: 43px;
    position: relative;
    display: flex;
    padding-bottom: 2px;
}

.sph-LiveInPlayHeader_Scroller{
    flex-grow: 1;
    min-width: 0;
    position: relative;
}

.sph-HorizontalLiveNavBarScroller_Wrapper{
    overflow-x: hidden;
    position: relative;
    padding-bottom: 2px;
    z-index: 10;
}

.sph-HorizontalLiveNavBarScroller_Contents {
    display: flex;
    position: relative;
    white-space: nowrap;
}

.sph-LiveInPlayButtonBarButton{
    padding: 0 13px 0 12px;
    padding-left: 20px;
    color: #fff;
    align-items: center;
    font-size: 13px;
    display: flex;
    flex-shrink: 0;
    height: 45px;
    line-height: 45px;
    cursor: pointer;
    position: relative;
}

.sm-SplashModule {
    position: relative;
}

.ff-FeaturedFixtureLoader {
    border-bottom: 0 solid transparent;
    background-color: #383838;
    border-top: 2px solid #367a65;
    padding-bottom: 10px;
}

.ff-Header {
    padding: 0 20px;
    color: #00FFB4;
    display: flex;
    margin: 0;
    height: 55px;
    align-items: center;
    line-height: 20px;
    justify-content: space-between;
}

.ff-Header_Title {
    font-size: 15px;
    font-weight: 700;
}

.ff-FeaturedFixtureScroller{
    padding: 0 20px 10px 15px;
}

.ff-FeaturedFixtureScroller_HScroll {
    display: flex;
    overflow-x: scroll;
    overflow-y: hidden;
}

.ff-FeaturedFixtureScroller_ScrollContent{
    left: 0px;
    transition: left .35s;
    white-space: nowrap;
    font-size: 0;
    width: 100%;
    display: flex;
    position: relative;
}

.ff-MarketGroupFixture {
    flex: 1 0 33.33%;
    padding-right: 0;
    margin-left: 0;
    height: 155px;
    border-radius: 5px;
    cursor: pointer;
    min-width: 270px;
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