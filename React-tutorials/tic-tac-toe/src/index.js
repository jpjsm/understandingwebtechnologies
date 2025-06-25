// React tutorial: 
// - https://reactjs.org/tutorial/tutorial.html
//
import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';

function Square(props) {
    return (
        <button
            className={props.winningSquare ? "winningSquare" : "square"}
            onClick={props.onClick}
        >
            {props.value}
        </button>
    );
}

class Board extends React.Component {
    renderSquare(i, winningSquare) {
        return (
            <Square
                value={this.props.squares[i] == null ? this.props.background : this.props.squares[i]}
                onClick={() => this.props.onClick(i)}
                winningSquare={winningSquare}
            />
        );
    }

    render() {
        return (
            <div>
                <div className="board-row">
                    {this.renderSquare(0, this.props.winningSquares.includes(0))}
                    {this.renderSquare(1, this.props.winningSquares.includes(1))}
                    {this.renderSquare(2, this.props.winningSquares.includes(2))}
                </div>
                <div className="board-row">
                    {this.renderSquare(3, this.props.winningSquares.includes(3))}
                    {this.renderSquare(4, this.props.winningSquares.includes(4))}
                    {this.renderSquare(5, this.props.winningSquares.includes(5))}
                </div>
                <div className="board-row">
                    {this.renderSquare(6, this.props.winningSquares.includes(6))}
                    {this.renderSquare(7, this.props.winningSquares.includes(7))}
                    {this.renderSquare(8, this.props.winningSquares.includes(8))}
                </div>
            </div>
        );
    }
}

class Game extends React.Component {
    constructor(props) {
        super(props);
        const mage = '\u{1F9D9}\u{200D}\u{2642}\u{FE0F}';
        const cat = '\u{1F63C}';
        const background = '\u{1F3DE}';
        const draw = '\u{1F6B7}';
        this.state = {
            mage: mage,
            cat: cat,
            background: background,
            draw: draw,
            message: '',
            history: [{
                squares: Array(9).fill(null),
            }],
            stepNumber: 0,
            xIsNext: true,
        };
    }

    handleClick(i) {
        const history = this.state.history.slice(0, this.state.stepNumber + 1);
        const current = history[this.state.stepNumber];
        const squares = current.squares.slice();
        let message = ' ';
        let [winner,] = calculateWinner(squares, this.state.draw)
        if (winner) {
            this.setState({
                message: 'Game finished, mo more moves allowed.'
            });
            return;
        }
        if (squares[i]) {
            this.setState({
                message: 'Pick a different cell; that one has already been played in.'
            });
            return;
        }

        squares[i] = this.state.xIsNext ? this.state.mage : this.state.cat;
        this.setState({
            history: history.concat([{ squares: squares }]),
            stepNumber: history.length,
            xIsNext: !this.state.xIsNext,
            message: message
        });
    }

    jumpTo(step) {
        this.setState({
            stepNumber: step,
            xIsNext: (step % 2) === 0,
        })
    }

    render() {
        const history = this.state.history;
        const current = history[this.state.stepNumber];
        const [winner, winningSquares] = calculateWinner(current.squares, this.state.draw)

        const moves = history.map((step, move) => {
            const desc = move ?
                'Go to move #' + move :
                'Go to game start';
            return (
                <li key={move}>
                    <button onClick={() => this.jumpTo(move)}>{desc}</button>
                </li>
            )
        });



        let status;
        if (winner) {
            status = 'Winner is: ' + winner
        }
        else {
            status = 'Next player: ' + (this.state.xIsNext ? this.state.mage : this.state.cat);
        }

        let message = this.state.message;

        return (
            <div className="game">
                <div className="game-board">
                    <Board
                        squares={current.squares}
                        onClick={(i) => this.handleClick(i)}
                        winningSquares={winningSquares}
                    />
                </div>
                <div className="game-info">
                    <div>{status}</div>
                    <hr />
                    <div>{message}</div>
                    <hr />
                    <ol>{moves}</ol>
                </div>
            </div>
        );
    }
}

// ========================================

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(<Game />);

// ******** other functions *********
function calculateWinner(squares, draw) {
    const lines = [
        [0, 1, 2],
        [3, 4, 5],
        [6, 7, 8],
        [0, 3, 6],
        [1, 4, 7],
        [2, 5, 8],
        [0, 4, 8],
        [2, 4, 6],
    ];

    for (let i = 0; i < lines.length; i++) {
        const [a, b, c] = lines[i];
        if (squares[a] && squares[a] === squares[b] && squares[a] === squares[c]) {
            return [squares[a], lines[i]];
        }
    }

    let full = true;
    for (let i = 0; i < squares.length; i++) {
        if (squares[i] == null) {
            full = false;
            break;
        }
    }

    if (full) {
        return draw;
    }
    return [null, null];
}