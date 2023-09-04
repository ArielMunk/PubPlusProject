import React from "react";
import Typography from '@mui/material/Typography';

function HelloUser (props){
    const user = props.user;
    const status_name = props.status_name;
    return (
        <Typography component="h1" variant="h5">
             Hello {user.user_full_name}, Your Status is:{status_name}
        </Typography>
    )
}

export default HelloUser;