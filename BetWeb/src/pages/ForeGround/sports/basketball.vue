<script>
import {onMounted, ref} from 'vue'
import axios from "axios"
import router from '../../../router/router'
export default{
    setup(){
        let schedule = ref([]);
        let displayBetResult = ref(false)
        let selectodds = ref(0)
        let selectteamname = ref("")
        let selectevent = ref({})
        let betMoney = ref(100)
        let prime = ref("")
        let betResultDisplay = ref(false)
        
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
        
        let selectTeam = (index, awayOrHome, odds) => {
            selectodds.value = odds
            selectteamname.value = awayOrHome
            displayBetResult.value = true
            selectevent.value = schedule.value[index]
            console.log(selectevent.value)
            betResultDisplay.value = true
        };

        let handleBetResultEmit = (value) => {
            displayBetResult.value = value; 
        }

        let sendCard = (event)=>{
            event.preventDefault()

            // 取得 TapPay Fields 的 status
            const tappayStatus = TPDirect.card.getTappayFieldsStatus()

            // 確認是否可以 getPrime
            if (tappayStatus.canGetPrime === false) {
                alert('can not get prime')
                return
            }

            // Get prime
            TPDirect.card.getPrime((result) => {
                if (result.status !== 0) {
                    alert('get prime error ' + result.msg)
                    return
                }
                alert('get prime 成功，prime: ' + result.card.prime)
                console.log(result)

                prime.value = result.card.prime

                axios.post(import.meta.env.VITE_API_URL + "/PostBets",{
                    "Bet": {
                        "EventId":selectevent.value.id,
                        "DateTime": getCurrentDateInFormat().toString(),
                        "Money": betMoney.value,
                        "AwayTeamName":selectevent.value.awayTeamName,
                        "AwayTeamOdds":selectevent.value.awayTeamOdds,
                        "HomeTeamName":selectevent.value.homeTeamName,
                        "HomeTeamOdds":selectevent.value.homeTeamOdds,
                        "Sports":selectevent.value.sports,
                        "BetTeamOdds":selectodds.value,
                        "BetTeamName":selectteamname.value,
                        "Prime":"",
                        "UserId":localStorage.getItem("userId")
                    },
                    "UserName":localStorage.getItem("username"),
                    "EventId":selectevent.value.id,
                    "Prime":prime.value
                },{
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`
                }})
                .then(res=>{
                    console.log(res)
                }).catch(err=>{
                    console.log(err)
                    localStorage.removeItem("token")
                    router.push({path:"/signin"})
                })
 
            })
        }
        onMounted(()=>{
            axios.get(import.meta.env.VITE_API_URL + "/GetEvents")
            .then(res=>{
                console.log(res)
                res.data.forEach(element => {
                    if (element.sports === "basketball") {
                        schedule.value.push(element);
                    }
                });
            })
            .catch(err=>{
                console.log(err)
            })


            // Display ccv field
            let fields = {
                number: {
                    // css selector
                    element: '#card-number',
                    placeholder: '**** **** **** ****'
                },
                expirationDate: {
                    // DOM object
                    element: '#card-expiration-date',
                    placeholder: 'MM / YY'
                },
                ccv: {
                    element: '#card-ccv',
                    placeholder: 'ccv'
                }
            };

            TPDirect.card.setup({
                fields: fields,
                styles: {
                    // Style all elements
                    'input': {
                        'color': 'gray'
                    },
                    // Styling ccv field
                    'input.ccv': {
                        // 'font-size': '16px'
                    },
                    // Styling expiration-date field
                    'input.card-expiration-date': {
                        // 'font-size': '16px'
                    },
                    // Styling card-number field
                    'input.card-number': {
                        // 'font-size': '16px'
                    },
                    // style focus state
                    ':focus': {
                        // 'color': 'black'
                    },
                    // style valid state
                    '.valid': {
                        'color': 'green'
                    },
                    // style invalid state
                    '.invalid': {
                        'color': 'red'
                    },
                    // Media queries
                    // Note that these apply to the iframe, not the root window.
                    '@media screen and (max-width: 400px)': {
                        'input': {
                            'color': 'orange'
                        }
                    }
                },
                // 此設定會顯示卡號輸入正確後，會顯示前六後四碼信用卡卡號
                isMaskCreditCardNumber: true,
                maskCreditCardNumberRange: {
                    beginIndex: 6,
                    endIndex: 11
                }
            })
            TPDirect.card.onUpdate(function (update) {
                // update.canGetPrime === true
                // --> you can call TPDirect.card.getPrime()
                if (update.canGetPrime) {
                    // Enable submit Button to get prime.
                    // submitButton.removeAttribute('disabled')
                } else {
                    // Disable submit Button to get prime.
                    // submitButton.setAttribute('disabled', true)
                }

                // cardTypes = ['mastercard', 'visa', 'jcb', 'amex', 'unionpay','unknown']
                if (update.cardType === 'visa') {
                    // Handle card type visa.
                }

                // number 欄位是錯誤的
                if (update.status.number === 2) {
                    // setNumberFormGroupToError()
                } else if (update.status.number === 0) {
                    // setNumberFormGroupToSuccess()
                } else {
                    // setNumberFormGroupToNormal()
                }

                if (update.status.expiry === 2) {
                    // setNumberFormGroupToError()
                } else if (update.status.expiry === 0) {
                    // setNumberFormGroupToSuccess()
                } else {
                    // setNumberFormGroupToNormal()
                }

                if (update.status.ccv === 2) {
                    // setNumberFormGroupToError()
                } else if (update.status.ccv === 0) {
                    // setNumberFormGroupToSuccess()
                } else {
                    // setNumberFormGroupToNormal()
                }
            })
        })
        return { 
            schedule, 
            displayBetResult, 
            selectodds, 
            selectteamname, 
            selectevent, 
            betMoney,
            betResultDisplay,
            updateBetMoney,
            selectTeam, 
            handleBetResultEmit,
            sendCard,
         };
    }
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
                                    <table class="sgl-MarketFixtureDetailsLabel-table">
                                        <tr style="background-color: #222;" class="sgl-MarketFixtureDetailsLabel-table-header">
                                            <th>日期</th>
                                            <th>時間</th>
                                            <th>隊伍</th>
                                            <th>賠率</th>
                                        </tr>
                                        <tr v-for="(item, index) in schedule" class="sgl-MarketFixtureDetailsLabel-table-content" v-show="!item.closeEvent">
                                            <td><div>{{ item.dateTime.split('T')[0] }}</div></td>
                                            <td>{{ item.dateTime.split('T')[1].split('Z')[0] }}</td>
                                            <td style="display: flex;">
                                                <span v-on:click="selectTeam(index, item.awayTeamName, item.awayTeamOdds)" style="width: 35%;" class="gl-MarketFixtureDetailsLabel-table-content-away"><a href="#creditCard" style="color: #fff;">{{ item.awayTeamName }}</a> </span>
                                                <span style="width: 5%;">{{ item.awayTeamPoint }}</span>
                                                <span style="width: 20%;">vs</span> 
                                                <span style="width: 5%;">{{ item.homeTeamPoint }}</span>
                                                <span v-on:click="selectTeam(index, item.homeTeamName, item.homeTeamOdds)" class="gl-MarketFixtureDetailsLabel-table-content-home" style="width: 35%;"><a href="#creditCard" style="color: #fff;">{{ item.homeTeamName }}</a></span></td>
                                            <td>{{ item.awayTeamOdds }} : {{ item.homeTeamOdds }}</td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                        <!-- <BetResult v-show="displayBetResult" v-bind:odds=selectodds v-bind:team=selectteamname v-bind:event="selectevent" @betResultEmit="handleBetResultEmit"></BetResult> -->
                        <div class="betResult" v-show="betResultDisplay">
                            <div class="betResult-content">
                                <div class="betResult-content-table">
                                    <table>
                                        <tr>
                                            <th>投注金額</th>
                                            <th>賠率</th>
                                            <th style="width:60%">投注隊伍</th>
                                            <!-- <th>讓分</th> -->
                                        </tr>
                                        <tr>
                                            <td>{{betMoney}}</td>
                                            <td>{{ selectodds }}</td>
                                            <td>{{ selectteamname }}</td>
                                            <!-- <td><input type="number" v-on:input="updateBetPoint"></td> -->
                                        </tr>
                                    </table>
                                </div>
                                <div style="display: flex;">
                                    <input type="range" class="betmoneyinputrange" min="100" max="5000" step="100" value="50" v-on:input="updateBetMoney">
                                </div>
                                <div style="display: flex;margin-top: 30px;" id="creditCard">
                                    <div style="display: flex;flex-direction: column;margin: auto;">
                                        <div style="margin: auto; color: #DDDDDD;">信用卡資料</div>
                                        <div class="tpfield" id="card-number"></div>
                                        <div class="tpfield" id="card-expiration-date"></div>
                                        <div class="tpfield" id="card-ccv"></div>
                                        <div v-on:click="sendCard" style="display: flex;" v-show="false">
                                            <div style="margin: auto;border: 1px solid gray;padding: 5px 10px;">確認</div>
                                        </div>
                                    </div>
                                </div>
                                <div class="betResult-content-surebutton">
                                    <div class="betResult-content-surebutton-content">
                                        <div class="betResult-content-surebutton-text" v-on:click="sendCard">確認</div>
                                        <!-- <div class="betResult-content-surebutton-textcancel" v-on:click="betResultButton(false, false)">取消</div> -->
                                    </div>
                                </div>
                            </div>
                            <!-- <div class="betResult-popup" v-show="betResultPopup">
                                <div class="betResult-popup-window">
                                    <div class="betResult-popup-window-header"></div>
                                    <div class="betResult-popup-window-body">
                                        <div style="color: #fff;text-align: center;margin-top: 100px;margin-bottom: 100px; cursor: default;">下注成功!</div>
                                        <div class="betResult-popup-window-body-button">
                                            <div class="betResult-popup-window-body-button-body">確認</div>
                                        </div>
                                    </div>
                                </div>
                            </div> -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
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

.betResult {
  border-top: 2px solid #367a65;
  background-color: #383838;
  /* position: absolute; */
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
    padding-bottom: 180px;
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

.tpfield {
    height: 40px;
    width: 300px;
    border: 1px solid gray;
    margin: 5px 0;
    padding: 5px;
}

.betResult-popup{
    position: absolute;
    width: 100%;
    height: 100%;
    z-index: 99;
    top: 0;
    background-color: #3838385c;
}

.betResult-popup-window{
    width: 500px;
    height:300px;
    background: #383838;
    border-radius: 5px;
    position: fixed;
    left: 40%;
    bottom: 15%;
}

.betResult-popup-window-header{
    width: 100%;
    height: 30px;
    background: #126e51;
    border-top-left-radius: 5px;
    border-top-right-radius: 5px;
}

.betResult-popup-window-body{
    width: 100%;
    height: calc(100% - 30px);
}

.betResult-popup-window-body-button{
    margin: auto;
    width: 50px;
    text-align: center;
}

.betResult-popup-window-body-button-body{
    padding: 5px;
    color: #fff;
    background: #126e51;
    border-radius: 5px;
    cursor: pointer;
}
</style>