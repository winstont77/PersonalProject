const routes = [
    {
        path:"/index",
        component:()=>import("../pages/index.vue"),
        children:[
            {
                path: 'basketball',
                component: ()=>import("../pages/sports/basketball/basketball.vue")
            },
        ]
    },
    {
        path:"/signup",
        component:()=>import("../pages/signup.vue")
    },
    {
        path:"/signin",
        component:()=>import("../pages/signin.vue")
    },
]

export default routes;