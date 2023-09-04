import * as React from 'react';
import { styled, createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import MuiDrawer from '@mui/material/Drawer';
import Box from '@mui/material/Box';
import MuiAppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import Badge from '@mui/material/Badge';
import Container from '@mui/material/Container';
import Grid from '@mui/material/Grid';
import Paper from '@mui/material/Paper';
import Link from '@mui/material/Link';
import MenuIcon from '@mui/icons-material/Menu';
import ChevronLeftIcon from '@mui/icons-material/ChevronLeft';
import NotificationsIcon from '@mui/icons-material/Notifications';
import MultiSelect from './MultiSelect';
import { Title } from '@material-ui/icons';
import UsersDataTable from './UsersDataTable';

import HelloUser from './HelloUser';
import UpdateStatus from './UpdateStatus';
import axios from 'axios';
const defaultTheme = createTheme();

export default function Dashboard() {

  const [statusSelected,setStatusSelected] = React.useState();
  const [user,setUser] = React.useState(0);
  const [userStatus,setUserStatus] = React.useState(0);
  const [genericStatuses,setGenericStatuses] = React.useState([]);
  const [rows,setRows] = React.useState([]);

  React.useEffect(()=>{
    axios.get("user")
    .then(res => 
        {
            console.log(res);
            if(res.status!==200){
                throw ("Error");
            }
            const userParse = JSON.parse(res.data);
            setUser(userParse);
            setUserStatus(userParse.status_name);
            setStatusSelected(userParse.status_id);
        })
    .catch(error => {
    console.log(error);
    })},[]);

    React.useEffect(()=>{
      axios.get("list/statuses")
      .then(res => 
          {
              if(res.status!==200){
                  throw ("Error");
              }
              setGenericStatuses(res.data);
          })
      .catch(error => {
      console.log(error);
      })},[]);

  function ChangeStatusHandler(e){

  }
  return (
    <ThemeProvider theme={defaultTheme}>
      <Box sx={{ display: 'flex' }}>
        <CssBaseline />
        <Box
          component="main"
          sx={{
            flexGrow: 1,
            height: '100vh',
            overflow: 'auto',
          }}
        >
          <Toolbar />
          <Container maxWidth="lg" sx={{ mt: 4, mb: 4 }}>
            <Grid container spacing={3}>
              <Grid item xs={12} md={12} lg={12}>
                <Paper
                  sx={{
                    p: 2,
                    display: 'flex',
                    flexDirection: 'column',
                  }}
                >
                    <HelloUser user={user} status_name={userStatus} />
                    <UpdateStatus selected={statusSelected} ChangeHandler={(value)=>setUserStatus(value)} genericStatuses={genericStatuses} />
                    
                </Paper>
              </Grid>
              <Grid item xs={12} md={8} lg={9}>
                <Paper
                  sx={{
                    p: 2,
                    display: 'flex',
                    flexDirection: 'column',
                  }}
                >
                  <span>List of employees:</span>

                  <UsersDataTable options={{"rows":rows,"setRows":(value)=>setRows(value)}} />
                </Paper>
              </Grid>
              <Grid item xs={12}>
                <Paper sx={{ p: 2, display: 'flex', flexDirection: 'column' }}>
                </Paper>
              </Grid>
            </Grid>
          </Container>
        </Box>
      </Box>
    </ThemeProvider>
  );
}