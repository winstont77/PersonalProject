<script>
import {onMounted, ref} from 'vue'
import axios from "axios"
import router from '../../router/router'
export default{
    setup(){
        let username = ref("")
        let betMoney = ref(0)
        let profit = ref(0)
        let displayUserDetailStatus = ref(false)
        let changeDisplayUserDetailStatus = ()=>{
            displayUserDetailStatus.value=!displayUserDetailStatus.value
        }
        let memberDetail = ()=>{
            axios.post(import.meta.env.VITE_API_URL + "/PostUserDetail"
            ,{Name:localStorage.getItem("username")}
            ,{
                headers: {
                    Authorization: `Bearer ${localStorage.getItem("token")}`
                }
            })
            .then(res=>{
                console.log(res)
                username.value = res.data.name
                betMoney.value = res.data.money
                profit.value = res.data.profit
                console.log(betMoney.value)
            })
            .catch(err=>{
                console.log(err)
                router.push({path:"/signin"})
            })
        }
        let signout = ()=>{
            localStorage.removeItem("username")
            localStorage.removeItem("token")
            changeDisplayUserDetailStatus()
            router.push({path:"/signout"})
        }

        let goto = (value)=>{
            router.push({path:`/index/${value}`})
        }
        onMounted(()=>{
            memberDetail()
            axios.get(import.meta.env.VITE_API_URL + "/GetBasketball")
            .then(res=>{
                console.log(res)
            })
            .catch(err=>{
                console.log(err)
            })
        })
        return {
            username,
            displayUserDetailStatus,
            username,
            betMoney,
            profit,
            changeDisplayUserDetailStatus,
            signout,
            goto
        }
    }
}
</script>

<template>
    <div class="wc-WebConsoleModule_SiteContainer">
        <div class="wc-WebConsoleModule_Header">
            <div class="hm-HeaderModule">
                <div>
                    <div class="um-MemberMenuModule_WidthState-2" v-show="displayUserDetailStatus">
                        <div class="um-MemberMenuModule_Menu">
                            <div class="um-Header ">
                                <div class="um-Header_InfoRow ">
                                    <div class="um-Header_LeftSideWrapper ">
                                        <div class="um-UserInfo ">
                                            <div class="um-UserInfo_AccountInfo ">
                                                <span class="um-UserInfo_UserName ">投注金額</span>
                                                <span class="um-UserInfo_AccountBalanceWrapper ">
                                                    <span class="um-UserInfo_Balance ">NT ${{ betMoney }}.00</span>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="um-Header_RightSideWrapper ">
                                    </div>
                                </div>
                                <div class="um-BalanceDropdown">
                                    <div class="um-BalanceDropdown_Container ">
                                        <div class="um-BalanceDropdown_LeftColumn ">
                                            <div class="um-BalanceDropdown_Cell">
                                                <div class="um-BalanceDropdown_Title ">獎金</div>
                                                <div class="um-BalanceDropdown_Value ">NT$ {{ profit }}.00</div>
                                            </div>
                                        </div>
                                        <div class="um-BalanceDropdown_RightColumn ">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="um-MemberMenuModule_AreaPlaceholder ">
                                <div class="um-MainMenu ">
                                    <div class="um-GeneralTab ">
                                        <div class="um-GeneralTab_LogoutOption ">
                                            <div class="ul-MembersLinkButton-wide">
                                                <div class="ul-MembersLinkButton_Text" v-on:click="signout">登出</div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="hm-ProductHeaderWide">
                    <div class="hm-ProductHeaderWide_Container">
                        <div class="hm-HeaderScroller ">
                            <div class="hm-HeaderScroller_LeftArrow"></div>
                            <div class="hm-HeaderScroller_HScroll ">
                                <div class="hm-ProductHeaderWide_ScrollContainer">
                                </div>
                            </div>
                            <div class="hm-HeaderScroller_RightArrow"></div>
                        </div>
                    </div>
                    <div class="hm-ProductHeaderWide_RHS">
                        <div class="hm-ProductHeaderWideResponsibleGambling ">有節制博彩</div>
                        <div class="hm-ProductHeaderWideHelp ">幫助</div>
                    </div>
                </div>
                <div class="hm-MainHeaderWide">
                    <div class="hm-MainHeaderLogoWide">
                        <div class="hm-MainHeaderLogoWide_Bet365LogoImage"></div>
                    </div>
                    <div class="hm-MainHeaderCentreWide">
                        <div class="hm-MainHeaderCentreWide_Link">
                            <div>投注歷史</div>
                        </div>
                        <div class="hm-MainHeaderCentreWide_Link" v-on:click="changeDisplayUserDetailStatus">
                            <div>我的資料</div>
                        </div>
                    </div>
                    <div class="hm-MainHeaderRHSLoggedOutWide">
                        <div class="hm-MainHeaderRHSLoggedOutWide_JoinContainer">
                            <div>註冊</div>
                        </div>
                        <div class="hm-MainHeaderRHSLoggedOutWide_LoginContainer">
                            <div>登錄</div>
                        </div>
                    </div>
                </div>
                <div class="hm-MembersMenuModuleContainer_DarkWash" v-show="displayUserDetailStatus"></div>
            </div>
        </div>
        <div class="wc-PageView">
            <div class="wc-PageView_Main">
                <div class="wcl-CommonElementStyle_NavContentContainer">
                    <div class="wcl-CommonElementStyle_WebNav">
                        <div class="wn-WebNavModule">
                            <div class="wn-Menu">
                                <div class="wn-PreMatchGroup">
                                    <div class="wl-OfferItem">
                                        <div class="wnd-WebNavModuleDefault_LHSOfferIcon"></div>
                                        <span>優惠</span>
                                    </div>
                                    <div class="wn-PreMatchGroup_HeaderText " style="">完整列表</div>
                                    <div class="wn-PreMatchItem ">
                                        <div class="wn-ClassificationIcon ">
                                            <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-1">
                                            </div>
                                        </div>
                                        <span>足球</span>
                                        <div class="wn-PreMatchItem_FavIcon"></div>
                                    </div>
                                    <div class="wn-PreMatchItem ">
                                        <div class="wn-ClassificationIcon ">
                                            <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-2">
                                            </div>
                                        </div>
                                        <span>美式足球</span>
                                        <div class="wn-PreMatchItem_FavIcon"></div>
                                    </div>
                                    <div class="wn-PreMatchItem ">
                                        <div class="wn-ClassificationIcon ">
                                            <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-3">
                                            </div>
                                        </div>
                                        <span>棒球</span>
                                        <div class="wn-PreMatchItem_FavIcon"></div>
                                    </div>
                                    <div class="wn-PreMatchItem " v-on:click="goto('basketball')">
                                        <div class="wn-ClassificationIcon ">
                                            <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-4">
                                            </div>
                                        </div>
                                        <span>籃球</span>
                                        <div class="wn-PreMatchItem_FavIcon"></div>
                                    </div>
                                    <div class="wn-PreMatchItem ">
                                        <div class="wn-ClassificationIcon ">
                                            <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-5">
                                            </div>
                                        </div>
                                        <span>乒乓球</span>
                                        <div class="wn-PreMatchItem_FavIcon"></div>
                                    </div>
                                    <div class="wn-PreMatchItem ">
                                        <div class="wn-ClassificationIcon ">
                                            <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-6">
                                            </div>
                                        </div>
                                        <span>冰上曲棍球</span>
                                        <div class="wn-PreMatchItem_FavIcon"></div>
                                    </div>
                                    <div class="wn-PreMatchItem ">
                                        <div class="wn-ClassificationIcon ">
                                            <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-7">
                                            </div>
                                        </div>
                                        <span>司諾克</span>
                                        <div class="wn-PreMatchItem_FavIcon"></div>
                                    </div>
                                    <div class="wn-PreMatchItem ">
                                        <div class="wn-ClassificationIcon ">
                                            <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-8">
                                            </div>
                                        </div>
                                        <span>排球</span>
                                        <div class="wn-PreMatchItem_FavIcon"></div>
                                    </div>
                                    <div class="wn-PreMatchItem ">
                                        <div class="wn-ClassificationIcon ">
                                            <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-9">
                                            </div>
                                        </div>
                                        <span>田徑</span>
                                        <div class="wn-PreMatchItem_FavIcon"></div>
                                    </div>
                                    <div class="wn-PreMatchItem ">
                                        <div class="wn-ClassificationIcon ">
                                            <div class="cis-ClassificationIconSmall" id="cis-ClassificationIconSmall-10">
                                            </div>
                                        </div>
                                        <span>賽狗</span>
                                        <div class="wn-PreMatchItem_FavIcon"></div>
                                    </div>
                                    <div></div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="wcl-PageContainer">
                        <!---->
                        <RouterView></RouterView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.wc-WebConsoleModule_SiteContainer {
}

.wc-WebConsoleModule_Header{
    position: sticky;
    width: 100%;
    top: 0;
    z-index: 50;
}

.hm-HeaderModule{
    width: 100%;
    z-index: 100;
    min-width: 320px;
    position: relative;
    display: flex;
    flex-direction: column;
}

.um-MemberMenuModule_WidthState-2{
    padding-bottom: 25px;
    text-align: right;
    display: block;
    position: absolute;
    width: 100%;
    max-width: 375px;
    z-index: 40;
    right: 0;
    top: 100%;
}

.um-MemberMenuModule_Menu{
    max-height: calc(100vh - 115px);
    overflow-y: scroll;
    overflow-x: hidden;
    overscroll-behavior: none;
    display: inline-block;
    width: 100%;
    max-width: 375px;
    text-align: left;
    background-color: #f8f9fa;
    transition: transform .15s;
    transform: translateZ(0);
    border-radius: 0 0 0 2px;
    box-shadow: 0 2px 10px 0 rgba(0,0,0,.5);
}

.um-Header {

}

.um-Header_InfoRow {
    display: inline-flex;
    width: 100%;
    font-size: 15px;
    line-height: 17px;
    padding: 20px 15px 0;
    white-space: nowrap;
}

.um-Header_LeftSideWrapper {
    display: flex;
    padding-right: 20px;
}

.um-UserInfo {
    display: inline-block;
    vertical-align: top;
}

.um-UserInfo_AccountInfo{
    cursor: default;
}

.um-UserInfo_UserName {
    width: 100%;
    display: block;
    font-size: 12px;
    color: #404040;
    line-height: 17px;
}

.um-UserInfo_AccountBalanceWrapper {
    width: 100%;
    display: inline-flex;
    line-height: 23px;
}

.um-UserInfo_Balance {
    font-size: 20px;
    font-weight: 700;
    color: #404040;
    display: inline-block;
    padding-right: 5px;
}

.um-Header_RightSideWrapper {
    display: flex;
    vertical-align: middle;
    text-align: right;
    margin-left: auto;
    margin-bottom: 15px;
    min-width: 120px;
}

.um-DepositButton {
    display: flex;
    vertical-align: middle;
    padding: 0 12px;
    border: 1px solid #c3c3c3;
    color: #404040;
    border-radius: 2px;
    height: 44px;
    cursor: pointer;
    font-size: 14px;
    font-weight: 700;
    line-height: 14px;
    min-width: 120px;
    align-items: center;
    justify-content: center;
    background-color: #fafbfc;
}

.um-DepositButton::before{
    content: "";
    width: 12px;
    height: 13px;
    margin-right: 10px;
    background-repeat: no-repeat;
    background-position: 50%;
    background-image: url(../images/topUp.svg);
}

.um-BalanceDropdown{
    width: 100%;
    padding: 0 15px 5px;
    border-bottom: 1px solid #d4d4d4;
}

.um-BalanceDropdown_Container{
    position: relative;
    display: flex;
}

.um-BalanceDropdown_LeftColumn {
    display: flex;
    flex-flow: column;
}

.um-BalanceDropdown_Cell{
    margin-top: 0;
    margin-bottom: 15px;
    display: inline-block;
    padding-right: 70px;
}

.um-BalanceDropdown_Title {
    line-height: 17px;
    font-size: 11px;
    color: #404040;
    white-space: nowrap;
}

.um-BalanceDropdown_Value {
    line-height: 20px;
    font-size: 15px;
    color: #404040;
    font-weight: 700;
}

.um-BalanceDropdown_RightColumn {
    display: flex;
    flex-flow: column;
}

.um-BalanceDropdown_Cell-bonus{
    margin-top: 0;
    margin-bottom: 15px;
    display: inline-block;
    padding-right: 70px;
}

.um-MemberMenuModule_AreaPlaceholder {

}

.um-MainMenu {

}

.um-GeneralTab {
    display: flex;
    flex-wrap: wrap;
}

.um-GeneralTab_AccountContainer{
    display: flex;
    flex-wrap: wrap;
    background-color: #f3f5f4;
    padding: 20px 0 10px 5px;
    justify-content: space-around;
    width: 100%;
}

.ul-MembersLinkButton{
    display: flex;
    flex-direction: column;
    align-items: center;
    font-size: 11px;
    font-weight: 700;
    color: #505050;
    line-height: 17px;
    margin-right: 5px;
    padding-bottom: 20px;
    min-width: 100px;
    cursor: pointer;
    text-align: center;
    flex: 1 0 31%;
    max-width: 31%;
}

.ul-MembersLinkButton_Icon{
    height: 39px;
    margin-bottom: 8px;
    display: flex;
    justify-content: center;
    align-items: flex-end;
}

.ul-MembersLinkButton_Text-2 {
    width: 100%;
    word-break: break-word;
    word-wrap: break-word;
    cursor: pointer;
    text-align: center;
    font-size: 11px;
    font-weight: 700;
    color: #505050;
    line-height: 17px;
}

.um-GeneralTab_LogoutOption {
    width: 100%;
}

.ul-MembersLinkButton-wide{
    height: 55px;
    padding-bottom: 5px;
    padding-top: 5px;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
    display: block;
    text-align: left;
    font-size: 15px;
    align-items: flex-start;
    width: 100%;
    margin-right: 0;
    font-weight: 400;
    color: #404040;
    line-height: 45px;
    padding-left: 15px;
    padding-right: 15px;
    min-width: 100px;
    cursor: pointer;
}

.ul-MembersLinkButton-wide:hover{
    background-color: #fff;
}

.ul-MembersLinkButton_Text {
    text-align: left;
    width: 100%;
    word-break: break-word;
    word-wrap: break-word;
}

.hm-ProductHeaderWide{
    margin-right: 15px;
    display: flex;
    justify-content: space-between;
    background-color: #333;
    font-size: 12px;
    line-height: 13px;
    width: 100%;
    height: 45px;
    padding: 0 calc((100vw - 1450px)/2);
}

.hm-ProductHeaderWide_Container {
    white-space: nowrap;
    margin-left: 20px;
    display: flex;
    overflow: hidden;
    align-items: center;
    align-self: center;
    position: relative;
    flex: 1 1 100%;
    font-size: 12px;
    line-height: 13px;
}

.hm-HeaderScroller {
    padding: 0;
    margin: 0;
    width: 100%;
    position: relative;
}

.hm-HeaderScroller_LeftArrow{
    opacity: 0;
    visibility: hidden;
    transition: opacity .4s,visibility .4s;
    transition-delay: .2s;
    width: 20px;
    background-color: #333;
    height: 100%;
    background-size: 7px 12px;
    background-position: 50%;
    background-repeat: no-repeat;
    position: absolute;
    top: 0;
    z-index: 10;
    cursor: pointer;
    left: 0;
    border-right: 1px solid #545454;
}

.hm-HeaderScroller_HScroll {
    white-space: nowrap;
    display: block;
}

.hm-ProductHeaderWide_ScrollContainer{
    overflow-y: auto;
    height: 30px;
}

.hm-ProductHeaderWide_Link{
    display: inline-flex;
    align-items: center;
    height: 100%;
    font-size: 12px;
    margin-right: 20px;
    color: #fff;
    cursor: pointer;
    transition-property: opacity;
    transition-duration: .2s;
}

.hm-HeaderScroller_RightArrow{
    opacity: 0;
    visibility: hidden;
    transition: opacity .4s,visibility .4s;
    transition-delay: .2s;
    width: 20px;
    background-color: #333;
    height: 100%;
    background-size: 7px 12px;
    background-position: 50%;
    background-repeat: no-repeat;
    position: absolute;
    top: 0;
    z-index: 10;
    cursor: pointer;
    right: 0;
    border-left: 1px solid #545454;
}

.hm-ProductHeaderWide_RHS{
    height: 100%;
    margin-right: 20px;
    display: flex;
    flex: 1 0 auto;
}

.hm-ProductHeaderWideResponsibleGambling {
    font-size: 12px;
    display: inline-flex;
    align-items: center;
    height: 100%;
    margin-right: 20px;
    color: #9c9c9c;
    cursor: pointer;
    flex-shrink: 0;
}

.hm-ProductHeaderWideResponsibleGambling:hover{
    color: #fff;
}

.hm-ProductHeaderWideHelp {
    font-size: 11px;
    display: inline-flex;
    align-items: center;
    height: 100%;
    margin-right: 20px;
    color: #9c9c9c;
    cursor: pointer;
    flex-shrink: 0;
    margin-right: 0;
}

.hm-ProductHeaderWideHelp:hover{
    color: #fff;
}

.hm-MainHeaderWide{
    z-index: 20;
    display: flex;
    align-items: center;
    position: relative;
    background-color: #126e51;
    height: 60px;
    justify-content: space-between;
    padding: 0 calc((100vw - 1450px)/2);
}

.hm-MainHeaderLogoWide{
    display: flex;
    height: 100%;
    flex: 0 1 25%;
}

.hm-MainHeaderLogoWide_Bet365LogoImage {
    background-repeat: no-repeat;
    background-position: 50%;
    height: 100%;
    position: relative;
    margin-left: 20px;
    width: 100px;
    background-size: contain;
    background-image: url(../../images/logos/indexLogo.svg);
    cursor: pointer;
}

.hm-MainHeaderCentreWide{
    font-size: 14px;
    height: 100%;
    flex: 0 0 50%;
    display: flex;
    align-items: center;
    position: absolute;
    padding: 0;
    top: 0;
    bottom: 0;
    left: 50%;
    transform: translateX(-50%);
}

.hm-MainHeaderCentreWide_Link{
    display: flex;
    position: relative;
    align-items: center;
    justify-content: center;
    text-align: center;
    color: #e4e4e4;
    text-decoration: none;
    margin: 0 22.5px;
    height: 100%;
    line-height: 100%;
    opacity: 1;
    cursor: pointer;
}

.hm-MainHeaderCentreWide_Link:hover{
    color: #26ffbe;
}

.hm-MainHeaderRHSLoggedOutWide {
    display: none;
    /* display: flex; */
    justify-content: flex-end;
    align-items: center;
    flex: 1 0 25%;
    height: 100%;
    white-space: nowrap;
    transition-property: opacity;
    transition-duration: .2s;
}

.hm-MainHeaderRHSLoggedOutWide_JoinContainer{
    border-radius: 2px;
    padding: 0 10px;
    height: 26px;
    color: #333;
    background-color: #ffdf1b;
    display: flex;
    align-items: center;
    margin-left: 20px;
    font-size: 13px;
    line-height: 17px;
    flex: 0 0 auto;
    cursor: pointer;
}

.hm-MainHeaderRHSLoggedOutWide_JoinContainer:hover{
    background-color: #e4e4e4;
}

.hm-MainHeaderRHSLoggedOutWide_LoginContainer{
    display: flex;
    align-items: center;
    color: #fff;
    margin: 0 20px 0 15px;
    font-size: 13px;
    line-height: 17px;
    height: 100%;
    cursor: pointer;
}

.hm-MainHeaderRHSLoggedOutWide_LoginContainer:hover{
    color: #26ffbe;
}

.hm-MainHeaderMembersWide{
    display: inline-flex;
    flex: 0 0 auto;
    height: 100%;
    flex-direction: row;
    align-items: center;
    justify-content: flex-end;
    min-width: 160px;
    padding-right: 20px;
}

.hm-MainHeaderMembersWide_MembersMenuIcon{
    display: flex;
    align-items: center;
    background-repeat: no-repeat;
    width: 32px;
    height: 32px;
    cursor: pointer;
    position: relative;
    background-image: url(../images/member.svg);
}

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

.wcl-CommonElementStyle_WebNav {
    flex: 1 0 270px;
    max-width: 270px;
    background-color: #282828;
    height: 100%;
}

.wn-WebNavModule{
    font-size: 13px;
    height: 100%;
    color: #ddd;
    font-size: 14px;
    line-height: 25px;
    overflow: hidden;
}

.wn-Menu{
    overflow-y: auto;
    overflow-x: hidden;
    overscroll-behavior: none;
    height: 100%;
}

.wn-PreMatchGroup{
    position: relative;
}

.wl-OfferItem{
    color: #fff;
    background-color: #282828;
    font-weight: 700;
    cursor: default;
    display: flex;
    height: 45px;
    margin-top: 20px;
    padding-left: 20px;
    position: relative;
    line-height: 45px;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
}

.wl-OfferItem:hover{
    background-color: #383838;
    color: #fff;
}

.wnd-WebNavModuleDefault_LHSOfferIcon{
    align-self: center;
    width: 15px;
    height: 15px;
    display: flex;
    margin-right: 10px;
    background-image: url(../images/sideMenu/offer.svg);
    background-size: 100%;
    background-repeat: no-repeat;
}

.wn-PreMatchGroup_HeaderText{
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
    line-height: 13px;
    padding: 32px 10px 10px 20px;
    font-size: 11px;
    text-transform: uppercase;
    color: #00DEA9;
    font-weight: 700;
    background-color: #282828;
    padding: 32px 0 10px 20px;
    cursor: default;
}

.wn-PreMatchItem {
    display: flex;
    align-items: center;
    height: 45px;
    position: relative;
    line-height: 45px;
    cursor: pointer;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
    padding-left: 20px;
}

.wn-PreMatchItem:hover{
    background-color: #383838;
    color: #fff;
}

.wn-ClassificationIcon {
    width: 15px;
    height: 15px;
    margin-right: 10px;
}

.cis-ClassificationIconSmall{
    background-position: 50%;
    background-repeat: no-repeat;
    background-size: contain;
    height: 18px;
    width: 18px;
}

.wn-PreMatchItem_FavIcon{
    display: block;
    height: 45px;
    width: 45px;
    position: absolute;
    top: 0;
    right: 0;
    cursor: pointer;
}

#cis-ClassificationIconSmall-1{
    background-image: url(../../images/sideMenu/soccer.svg);
}

#cis-ClassificationIconSmall-2{
    background-image: url(../../images/sideMenu/football.svg);
}

#cis-ClassificationIconSmall-3{
    background-image: url(../../images/sideMenu/baseball.svg);
}

#cis-ClassificationIconSmall-4{
    background-image: url(../../images/sideMenu/basketball.svg);
}

#cis-ClassificationIconSmall-5{
    background-image: url(../../images/sideMenu/tabletennis.svg);
}

#cis-ClassificationIconSmall-6{
    background-image: url(../../images/sideMenu/icehockey.svg);
}

#cis-ClassificationIconSmall-7{
    background-image: url(../../images/sideMenu/snooker.svg);
}

#cis-ClassificationIconSmall-8{
    background-image: url(../../images/sideMenu/volleyball.svg);
}

#cis-ClassificationIconSmall-9{
    background-image: url(../../images/sideMenu/athletics.svg);
}

#cis-ClassificationIconSmall-10{
    background-image: url(../../images/sideMenu/greyhoundracing.svg);
}

.wcl-PageContainer{
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 100%;
    overflow-y: auto;
    overflow-x: hidden;
    overscroll-behavior: none;
}

.hm-MembersMenuModuleContainer_DarkWash {
    background-color: #000;
    opacity: .6;
    position: fixed;
    top: 105px;
    left: 0;
    bottom: 0;
    right: 0;
    z-index: -1;
    height: calc(100vh - 105px);
}

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

.wcl-CommonElementStyle_WebNav {
    flex: 1 0 270px;
    max-width: 270px;
    background-color: #282828;
    height: 100%;
}

.wn-WebNavModule{
    font-size: 13px;
    height: 100%;
    color: #ddd;
    font-size: 14px;
    line-height: 25px;
    overflow: hidden;
}

.wn-Menu{
    overflow-y: auto;
    overflow-x: hidden;
    overscroll-behavior: none;
    height: 100%;
}

.wn-PreMatchGroup{
    position: relative;
}

.wl-OfferItem{
    color: #fff;
    background-color: #282828;
    font-weight: 700;
    cursor: default;
    display: flex;
    height: 45px;
    margin-top: 20px;
    padding-left: 20px;
    position: relative;
    line-height: 45px;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
}

.wl-OfferItem:hover{
    background-color: #383838;
    color: #fff;
}

.wnd-WebNavModuleDefault_LHSOfferIcon{
    align-self: center;
    width: 15px;
    height: 15px;
    display: flex;
    margin-right: 10px;
    background-image: url(../../images/sideMenu/offer.svg);
    background-size: 100%;
    background-repeat: no-repeat;
}

.wn-PreMatchGroup_HeaderText{
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
    line-height: 13px;
    padding: 32px 10px 10px 20px;
    font-size: 11px;
    text-transform: uppercase;
    color: #00DEA9;
    font-weight: 700;
    background-color: #282828;
    padding: 32px 0 10px 20px;
    cursor: default;
}

.wn-PreMatchItem {
    display: flex;
    align-items: center;
    height: 45px;
    position: relative;
    line-height: 45px;
    cursor: pointer;
    text-overflow: ellipsis;
    overflow: hidden;
    white-space: nowrap;
    padding-left: 20px;
}

.wn-PreMatchItem:hover{
    background-color: #383838;
    color: #fff;
}

.wn-ClassificationIcon {
    width: 15px;
    height: 15px;
    margin-right: 10px;
}

.cis-ClassificationIconSmall{
    background-position: 50%;
    background-repeat: no-repeat;
    background-size: contain;
    height: 18px;
    width: 18px;
}

.wn-PreMatchItem_FavIcon{
    display: block;
    height: 45px;
    width: 45px;
    position: absolute;
    top: 0;
    right: 0;
    cursor: pointer;
}

#cis-ClassificationIconSmall-1{
    background-image: url(../../images/sideMenu/soccer.svg);
}

#cis-ClassificationIconSmall-2{
    background-image: url(../../images/sideMenu/football.svg);
}

#cis-ClassificationIconSmall-3{
    background-image: url(../../images/sideMenu/baseball.svg);
}

#cis-ClassificationIconSmall-4{
    background-image: url(../../images/sideMenu/basketball.svg);
}

#cis-ClassificationIconSmall-5{
    background-image: url(../../images/sideMenu/tabletennis.svg);
}

#cis-ClassificationIconSmall-6{
    background-image: url(../../images/sideMenu/icehockey.svg);
}

#cis-ClassificationIconSmall-7{
    background-image: url(../../images/sideMenu/snooker.svg);
}

#cis-ClassificationIconSmall-8{
    background-image: url(../../images/sideMenu/volleyball.svg);
}

#cis-ClassificationIconSmall-9{
    background-image: url(../../images/sideMenu/athletics.svg);
}

#cis-ClassificationIconSmall-10{
    background-image: url(../../images/sideMenu/greyhoundracing.svg);
}

.wcl-PageContainer{
    display: flex;
    flex-direction: column;
    width: 100%;
    height: 100%;
    overflow-y: auto;
    overflow-x: hidden;
    overscroll-behavior: none;
}
</style>