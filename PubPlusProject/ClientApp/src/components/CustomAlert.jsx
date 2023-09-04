import React,{useState,useContext} from "react";
import { Alert,AlertTitle } from "@mui/material";

function CustomAlert(props){
    const typeAlert = props.typeAlert;
    const setTypeAlert = props.setTypeAlert;
    if(typeof(typeAlert)==="undefined")
    {
        return null; 
    }
    let title,severity,text;
    switch (typeAlert){
        case "success":{
            severity = "success";
            title = "Success!";
            text="";
            break;
        }
        case "error":{
            severity = "error";
            title = "Error!"
            text="";
            break;
        }
        default:
            return null;
    }
    setTimeout(()=>setTypeAlert(null),2500);
    return (
    <Alert severity={severity}>
        <AlertTitle>{title}</AlertTitle>
        {text}
     </Alert>
    );
}
export default CustomAlert;