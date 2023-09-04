const routes = [
    /* 前台 */
    {
        path:"/",
        component:()=>import("../pages/ForeGround/index.vue"),
    },
    {
        path:"/envTest",
        component:()=>import("../pages/ForeGround/envTest.vue"),
    },
    {
        path:"/sports/basketball",
        component:()=>import("../pages/ForeGround/sports/basketball.vue"),
    },
    {
        path:"/signout",
        component:()=>import("../pages/ForeGround/signout.vue"),
    },
    {
        path:"/signin",
        component:()=>import("../pages/ForeGround/signin.vue"),
    },
    {
        path:"/signup",
        component:()=>import("../pages/ForeGround/signup.vue"),
    },
    {
        path:"/index",
        component:()=>import("../pages/ForeGround/index.vue"),
        children:[
            {
                path:"basketball",
                component:()=>import("../pages/ForeGround/sports/basketball.vue"),
            },
        ]
    },
    /* 後台 */
    {
        path:"/cms/index",
        component:()=>import("../pages/CMS/index.vue"),
        children:[
            {
                path:"tablelist",
                component:()=>import("../pages/CMS/tablelist.vue"),
            },
        ]
    },
    /* 資料來源 */
    {
        path:"/sportData",
        component:()=>import("../pages/SportData/sportData.vue"),
    },
]

export default routes;