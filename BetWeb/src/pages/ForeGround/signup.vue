<script>
import { onMounted, ref } from 'vue';
import { reactive } from 'vue';
import router from "../../router/router.js"
import axios from "axios"

export default{
    setup(){
        let username = ref("")
        let password = ref(null)
        let repeatSignupDisplay = ref(false)
        let errorContent = ref("")
        let signup = ()=>{
            console.log(username.value, password.value)
            axios.post(import.meta.env.VITE_API_URL +　"/PostUsers", {
                Name:username.value,
                Password:password.value
            })
            .then(res=>{
                console.log(res)
                router.push({path:"signout"})
            })
            .catch(err=>{
                console.log(err)
                if(err.response.data==="User with name www already exists."){
                    repeatSignupDisplay.value = true
                    errorContent.value = "使用者名稱重複"
                }else{
                    repeatSignupDisplay.value = true
                    errorContent.value = "使用者名稱或密碼錯誤"
                }
            })
        }
        let gotoIndex = ()=>{
            router.push({path:"signout"})
        }
        onMounted(()=>{
            
        })
        return {
            username,
            password,
            repeatSignupDisplay,
            errorContent,
            signup,
            gotoIndex
        }
    }
}
</script>
<template>
    <div>
        <div class="signup">
            <div class="signup-element">
                <div class="signup-element-header-background">
                    <div class="signup-element-header-background-logo" v-on:click="gotoIndex"></div>
                </div>
                <div class="signup-element-content">
                    <div class="signup-element-content-container">
                        <div class="signup-element-content-container-title">
                            <div class="signup-element-content-container-title-container">
                                <div class="signup-element-content-container-title-container-text">開通帳戶</div>
                            </div>
                        </div>
                        <div>
                            <div class="oam-OpenAccountModule">
                                <div class="oam-OpenAccountModule_Form">
                                    <div class="oam-FieldGroupWithBorderTopText">
                                        <div class="oam-FieldGroupWithBorderTopText_Header">創建登陸</div>
                                        <div class="oam-FieldInputNewUsername">
                                            <div class="oam-FieldInputNewUsername_InputRow" id="oam-FieldInputNewUsername_InputRow" v-on:click="focusName" >
                                                <input type="text" class="oam-FieldInputNewUsername_Input" placeholder="用戶名" v-model="username">
                                                <div class="oam-FieldInputNewUsername_InfoAnchor">
                                                    <div class="oam-FieldInputNewUsername_Tooltip" id="oam-FieldInputNewUsername_Tooltip">
                                                        <div class="oam-FieldInputNewUsername_TooltipText">
                                                            僅可包含字母、數字和下劃線，不可帶空格或重音符號。
                                                        </div>
                                                    </div>
                                                    <div class="oam-FieldInputNewUsername_InfoButton" v-on:click="exclamationSignName"></div>
                                                    <div class="oam-FieldInputNewUsername_RightText">
                                                        6至14個字符
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="oam-FieldInputNewPassword">
                                            <div class="oam-FieldInputNewPassword_InputRow" id="oam-FieldInputNewPassword_InputRow" v-on:click="focusPassword">
                                                <input type="password" class="oam-FieldInputNewPassword_Input" id="oam-FieldInputNewPassword_Input" placeholder="密碼" v-model="password">
                                                <!-- <span class="oam-FieldInputNewPassword_MaskToggle" v-on:click="displayPassword">顯示</span> -->
                                                <div class="oam-FieldInputNewPassword_InfoAnchor">
                                                    <div class="oam-FieldInputNewPassword_Tooltip" id="oam-FieldInputNewPassword_Tooltip">
                                                        <div class="oam-FieldInputNewPassword_TooltipText ">不可包含用戶名、姓名、電子郵箱或出生年份。</div>
                                                        <div class="oam-FieldInputNewPassword_TooltipText ">必須包含6-32個字符。</div>
                                                        <div class="oam-FieldInputNewPassword_TooltipText ">請用下列組合增強密碼的安全性：</div>
                                                        <div class="oam-FieldInputNewPassword_TooltipText ">數字</div>
                                                        <div class="oam-FieldInputNewPassword_TooltipText ">大寫字母</div>
                                                        <div class="oam-FieldInputNewPassword_TooltipText ">小寫字母</div>
                                                        <div class="oam-FieldInputNewPassword_TooltipText ">!"#$%&amp;'()*+,-./:;&lt;=&gt;?_@[\]^`{|}~</div>
                                                    </div>
                                                    <div class="oam-FieldInputNewPassword_InfoButton" v-on:click="exclamationSignPassword"></div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="oam-FieldInputCheckboxTerms ">
                                            <div class="oam-FieldInputCheckboxTerms_Container">
                                                <!-- <label for="" class="oam-FieldInputCheckboxTerms_Checkbox" id="oam-FieldInputCheckboxTerms_Checkbox" v-on:click="checkTerms"></label> -->
                                                <div class="oam-FieldInputCheckboxTerms_Label">
                                                    <div>
                                                        <span>我已年滿18周歲，且已閱讀、接受並同意</span>
                                                        <span class="oam-FieldInputCheckboxTerms_Link ">條款與規則</span><span>、</span>
                                                        <span class="oam-FieldInputCheckboxTerms_Link ">規則</span><span>、</span>
                                                        <span class="oam-FieldInputCheckboxTerms_Link ">隱私政策</span><span>、</span>
                                                        <span class="oam-FieldInputCheckboxTerms_Link ">Cookies政策</span><span>以及</span>
                                                        <span class="oam-FieldInputCheckboxTerms_Link ">年齡驗證</span><span>和</span>
                                                        <span class="oam-FieldInputCheckboxTerms_Link ">身份驗證</span><span>的相關政策。</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="signuperrorprompt" v-show="repeatSignupDisplay">{{ errorContent }}</div>
                                        <button class="oam-OAFieldSubmitButton " v-on:click="signup">加入</button>
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
.signup{

}

.signup-element{
    display: flex;
    flex-direction: column;
}

.signup-element-header-background{
    align-items: center;
    position: relative;
    display: flex;
    height: 50px;
    background-color: #126e51;
    justify-content: space-between;
}

.signup-element-header-background-logo{
    margin-left: 20px;
    height: 22px;
    width: 100px;
    flex-shrink: 1;
    flex-grow: 0;
    background-image: url(../../images/logos/signupLogo.svg);
    background-repeat: no-repeat;
    background-size: contain;
    background-position: 50%;
    cursor: pointer;
}

.signup-element-content{
    justify-content: center;
    position: relative;
    flex: 1 0 auto;
    max-width: 1080px;
    margin: 0 auto;
    width: 100%;
    display: flex;
}

.signup-element-content-container{
    padding-bottom: 70px;
    flex: 0 1 530px;
    width: 100%;
}

.signup-element-content-container-title-container{
    cursor: default;
    height: auto;
    padding: 35px 15px 25px;
    display: flex;
    font-size: 22px;
    text-align: left;
    color: #282828;
    line-height: 32px;
    font-weight: 700;
    justify-content: space-between;
    min-height: 70px;
    position: relative;
}

.signup-element-content-container-title-container-text{
    padding-right: 0;
}

.oam-OpenAccountModule {
    width: 100%;
    padding: 0 15px;
}

.oam-FieldGroupWithBorderTopText {

}

.oam-FieldGroupWithBorderTopText_Header {
    font-size: 15px;
    font-weight: 700;
    color: #282828;
    line-height: 21px;
    margin-bottom: 15px;
    position: relative;
}

.oam-FieldInputNewUsername {
    display: flex;
    position: relative;
    flex-flow: column;
    margin: 0 0 15px;
}

.oam-FieldInputNewUsername_InputRow {
    flex-flow: row;
    background: #fff;
    border-radius: 2px;
    box-shadow: 0 1px 0 0 #d4d4d4;
    position: relative;
    display: flex;
}

.oam-FieldInputNewUsername_Input{
    flex: 1;
    height: 50px;
    padding-left: 15px;
    border: 0;
    color: #282828;
    background-color: #fff;
    font-size: 16px;
    font-weight: 700;
    line-height: 23px;
    border-radius: 2px 0 0 2px;
    overflow: visible;
}

.oam-FieldInputNewUsername_InfoAnchor {

}

.oam-FieldInputNewUsername_Tooltip{
    font-size: 12px;
    color: #282828;
    line-height: 18px;
    background: #f5f7f7;
    border: 1px solid #b5b5b5;
    border-radius: 2px;
    z-index: 110;
    margin: 0 10px;
    position: absolute;
    right: 0;
    top: 48px;
    overflow-wrap: break-word;
    padding: 15px;
    margin-right: 0;
    display: none;
}

.oam-FieldInputNewUsername_Tooltip::before{
    right: 14px;
    content: "";
    position: absolute;
    bottom: 100%;
    width: 0;
    height: 0;
    border: 7px solid transparent;
    border-bottom: 7px solid #b5b5b5;
}

.oam-FieldInputNewUsername_TooltipText{
    font-size: 12px;
    color: #282828;
    line-height: 18px;
    overflow-wrap: break-word;
}

.oam-FieldInputNewUsername_InfoButton {
    display: block;
    cursor: pointer;
    height: 50px;
    width: 45px;
    border: 0;
    padding: 0;
    padding-left: 15px;
    border-radius: 0 2px 2px 0;
    background-color: #fff;
    background-image: url(../images/exclamation.svg);
    background-repeat: no-repeat;
    background-position: 50%;
}

.oam-FieldInputNewUsername_RightText{
    right: 30px;
    font-size: 12px;
    color: #757575;
    padding: 0 15px;
    text-align: right;
    background-color: transparent;
    position: absolute;
    top: 50%;
    transform: translateY(-50%);
    border: 0;
    transition: opacity .15s ease 0ms;
    opacity: 1;
}

.oam-FieldInputNewPassword {
    display: flex;
    flex-flow: column;
    position: relative;
}

.oam-FieldInputNewPassword_InputRow {
    position: relative;
    display: flex;
    flex-flow: row;
    background: #fff;
    border-radius: 2px;
    box-shadow: 0 1px 0 0 #d4d4d4;
    margin-bottom: 15px;
}

.oam-FieldInputNewPassword_Input{
    flex: 1;
    height: 50px;
    padding-left: 15px;
    border: 0;
    color: #282828;
    background-color: #fff;
    font-size: 16px;
    font-weight: 700;
    line-height: 23px;
    border-radius: 2px 0 0 2px;
    overflow: visible;
}

.oam-FieldInputNewPassword_MaskToggle {
    height: 50px;
    border: 0;
    padding: 0 10px 0 15px;
    background-color: #fff;
    font-weight: 700;
    font-size: 12px;
    cursor: pointer;
    border-radius: 0;
    display: flex;
    align-items: center;
    color: #126e51;
}

.oam-FieldInputNewPassword_InfoAnchor {

}

.oam-FieldInputNewPassword_Tooltip{
    margin-right: 0;
    font-size: 12px;
    color: #282828;
    line-height: 18px;
    background: #f5f7f7;
    border: 1px solid #b5b5b5;
    border-radius: 2px;
    z-index: 110;
    margin: 0 10px;
    position: absolute;
    right: 0;
    top: 48px;
    overflow-wrap: break-word;
    padding: 15px;
    display: none;
}

.oam-FieldInputNewPassword_Tooltip::before{
    right: 14px;
    content: "";
    position: absolute;
    bottom: 100%;
    width: 0;
    height: 0;
    border: 7px solid transparent;
    border-bottom: 7px solid #b5b5b5;
}

.oam-FieldInputNewPassword_Tooltip::after{
    right: 15px;
    border: 6px solid transparent;
    border-bottom: 6px solid #f5f7f7;
    content: "";
    position: absolute;
    bottom: 100%;
    width: 0;
    height: 0;
}

.oam-FieldInputNewPassword_TooltipText{
    font-size: 12px;
    color: #282828;
    line-height: 18px;
}

.oam-FieldInputNewPassword_InfoButton {
    display: block;
    cursor: pointer;
    height: 50px;
    width: 45px;
    border: 0;
    padding: 0;
    padding-left: 15px;
    border-radius: 0 2px 2px 0;
    background-color: #fff;
    background-image: url(../images/exclamation.svg);
    background-repeat: no-repeat;
    background-position: 50%;
}

.oam-FieldInputCheckboxTerms {

}

.oam-FieldInputCheckboxTerms_Container {
    display: inline-flex;
    position: relative;
}

.oam-FieldInputCheckboxTerms_Checkbox {
    width: 46px;
    height: 46px;
    margin-left: -13px;
    display: flex;
    flex-shrink: 0;
    align-items: center;
    justify-content: center;
    cursor: pointer;
}

.oam-FieldInputCheckboxTerms_Checkbox::after{
    background-position: 50%;
    background-repeat: no-repeat;
    border: 0;
    content: "";
    display: block;
    background: #fff;
    width: 20px;
    border-radius: 2px;
    flex-shrink: 0;
    height: 20px;
    border: 1px solid #b5b5b5;
    /* background-image: url(../images/check.svg);
    background-color: #126e51;
    background-size: 8px;
    background-position: 50%;
    background-repeat: no-repeat; */
    background-image: var(--custom-background-image);
    background-color: var(--custom-background-color);
    background-size: var(--custom-background-size);
    background-position: var(--custom-background-position);
    background-repeat: var(--custom-background-repeat);
}

.signuperrorprompt{
    margin-top: 30px;
    color: crimson;
}

.oam-OAFieldSubmitButton {
    margin-bottom: 30px;
    height: 50px;
    width: 100%;
    padding: 0 15px;
    border: none;
    border-radius: 2px;
    cursor: pointer;
    font-size: 15px;
    line-height: 18px;
    align-items: center;
    justify-content: center;
    display: flex;
    font-weight: 700;
    text-align: center;
    color: #fff;
    background-color: #FF6B0F;
}

.oam-FieldInputCheckboxTerms_Label {
    padding-top: 13px;
    font-size: 11px;
    color: #282828;
    line-height: 17px;
    padding-bottom: 13px;
}

.oam-FieldInputCheckboxTerms_Link{
    color: #FF6B0F;
    font-weight: 700;
    text-decoration: none;
    cursor: pointer;
    line-height: 17px;
}

.oam-FieldInputCheckboxTerms_Link:hover{
    text-decoration: underline;
}
</style>