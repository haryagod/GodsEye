import { Button } from "@mui/material";
import { Component } from "react";
import TextFieldInput from "./textfield";

class Login extends Component {
    state = { 
        email:'',
        password:''
     } 

     handleInput=(e)=>
     {
         const state ={...this.state};
        state[e.target.name]=e.target.value;
        this.setState({...state});
     }


    render() { 
        return (
        <form className="form">

         <TextFieldInput id="loginemail"  name='email' label='Email' onValueChange={this.handleInput} value={this.state.email} required={true}>
             </TextFieldInput>   
             <TextFieldInput id="password" name='password' label='Password' onValueChange={this.handleInput} value={this.state.password} required={true}>
             </TextFieldInput>   
             <Button sx={{ ml: 5,mt:3, width: '15ch',justifyItems:"center" ,justifyContent:"center" }} variant="contained" >Login</Button>
        </form>);
    }
}
 
export default Login;