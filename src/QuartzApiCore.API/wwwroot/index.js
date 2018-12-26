import React from "react";
import ReactDOM from 'react-dom';
import {Container} from 'react-bootstrap';
import 'bootstrap/dist/css/bootstrap.css';
import configureStore from './store/configureStore';
import Root from './routes';
import {setJobs, fetchJobs} from "./actionCreators";
import {Provider} from 'react-redux';
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faStroopwafel, faPause, faPlay } from '@fortawesome/free-solid-svg-icons'

library.add(faStroopwafel);
library.add(faPause);
library.add(faPlay);

const store = configureStore();

store.dispatch(fetchJobs);

const App = ()=>
{
    return (
        <Provider store={store}>
        <Root />
        </Provider>
)
}

ReactDOM.render(<App />, document.getElementById('root'));