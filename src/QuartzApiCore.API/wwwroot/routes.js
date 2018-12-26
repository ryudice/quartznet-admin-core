import React from 'react';
import {
    HashRouter as Router,
    Route,
    Link
} from 'react-router-dom';
import {Container, Nav, NavItem, Navbar} from 'react-bootstrap';

import Jobs from './components/Jobs';

const Root = () => {
    return (<Router>
            <Container>
                <Navbar>
                    <Navbar.Brand>
                        <Link to="/">QuartzAdmin</Link>
                    </Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav"/>
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav>
                            <Nav.Item >
                                <Nav.Link as={Link} href="/jobs" to="/jobs">
                                    Jobs
                                </Nav.Link>
                            </Nav.Item>
                            <Nav.Item>
                                <Nav.Link as={Link} href="/triggers" to="/triggers">
                                    Triggers
                                </Nav.Link>
                            </Nav.Item>
                        </Nav></Navbar.Collapse>
                </Navbar>


                <Route exact path="/" component={Jobs}/>
                <Route path="/jobs" component={Jobs}/>
                <Route path="/triggers" component={Jobs}/>
                <Route path="/logs" component={Jobs}/>

            </Container>
        </Router>
    );
}

export default Root;