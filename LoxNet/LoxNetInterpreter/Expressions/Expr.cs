using LoxNetInterpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoxNetInterpreter.Expressions
{
    internal abstract class Expr
    {
        internal interface IVisitor<out R>
        {
            R VisitAssignExpr(Assign expr);
            R VisitBinaryExpr(Binary expr);
            R VisitCallExpr(Call expr);
            R VisitGetExpr(Get expr);
            R VisitGroupingExpr(Grouping expr);
            R VisitLiteralExpr(Literal expr);
            R VisitLogicalExpr(Logical expr);
            R VisitSetExpr(Set expr);
            R VisitSuperExpr(Super expr);
            R VisitThisExpr(This expr);
            R VisitUnaryExpr(Unary expr);
            R VisitVariableExpr(Variable expr);
        }

        // expr-assign
        internal sealed class Assign : Expr
        {
            public Assign(Token name, Expr value)
            {
                Name = name;
                Value = value;
            }

            public Token Name { get; }
            public Expr Value { get; }

            public override R Accept<R>(IVisitor<R> visitor) => visitor.VisitAssignExpr(this);
        }

        // expr-binary
        internal sealed class Binary : Expr
        {
            public Binary(Expr left, Token @operator, Expr right)
            {
                Left = left;
                Operator = @operator;
                Right = right;
            }

            public Expr Left { get; }
            public Token Operator { get; }
            public Expr Right { get; }

            public override R Accept<R>(IVisitor<R> visitor) => visitor.VisitBinaryExpr(this);
        }

        // expr-call
        internal sealed class Call : Expr
        {
            public Call(Expr callee, Token paren, IReadOnlyList<Expr> arguments)
            {
                Callee = callee;
                Paren = paren;
                Arguments = arguments;
            }

            public Expr Callee { get; }
            public Token Paren { get; }
            public IReadOnlyList<Expr> Arguments { get; }

            public override R Accept<R>(IVisitor<R> visitor) => visitor.VisitCallExpr(this);
        }

        // expr-get
        internal sealed class Get : Expr
        {
            public Get(Expr @object, Token name)
            {
                Object = @object;
                Name = name;
            }

            public Expr Object { get; }
            public Token Name { get; }

            public override R Accept<R>(IVisitor<R> visitor) => visitor.VisitGetExpr(this);
        }

        // expr-grouping
        internal sealed class Grouping : Expr
        {
            public Grouping(Expr expression)
            {
                Expression = expression;
            }

            public Expr Expression { get; }

            public override R Accept<R>(IVisitor<R> visitor) => visitor.VisitGroupingExpr(this);
        }

        // expr-literal
        internal sealed class Literal : Expr
        {
            public Literal(object? value)
            {
                Value = value;
            }

            public object? Value { get; }

            public override R Accept<R>(IVisitor<R> visitor) => visitor.VisitLiteralExpr(this);
        }

        // expr-logical
        internal sealed class Logical : Expr
        {
            public Logical(Expr left, Token @operator, Expr right)
            {
                Left = left;
                Operator = @operator;
                Right = right;
            }

            public Expr Left { get; }
            public Token Operator { get; }
            public Expr Right { get; }

            public override R Accept<R>(IVisitor<R> visitor) => visitor.VisitLogicalExpr(this);
        }

        // expr-set
        internal sealed class Set : Expr
        {
            public Set(Expr @object, Token name, Expr value)
            {
                Object = @object;
                Name = name;
                Value = value;
            }

            public Expr Object { get; }
            public Token Name { get; }
            public Expr Value { get; }

            public override R Accept<R>(IVisitor<R> visitor) => visitor.VisitSetExpr(this);
        }

        // expr-super
        internal sealed class Super : Expr
        {
            public Super(Token keyword, Token method)
            {
                Keyword = keyword;
                Method = method;
            }

            public Token Keyword { get; }
            public Token Method { get; }

            public override R Accept<R>(IVisitor<R> visitor) => visitor.VisitSuperExpr(this);
        }

        // expr-this
        internal sealed class This : Expr
        {
            public This(Token keyword)
            {
                Keyword = keyword;
            }

            public Token Keyword { get; }

            public override R Accept<R>(IVisitor<R> visitor) => visitor.VisitThisExpr(this);
        }

        // expr-unary
        internal sealed class Unary : Expr
        {
            public Unary(Token @operator, Expr right)
            {
                Operator = @operator;
                Right = right;
            }

            public Token Operator { get; }
            public Expr Right { get; }

            public override R Accept<R>(IVisitor<R> visitor) => visitor.VisitUnaryExpr(this);
        }

        // expr-variable
        internal sealed class Variable : Expr
        {
            public Variable(Token name)
            {
                Name = name;
            }

            public Token Name { get; }

            public override R Accept<R>(IVisitor<R> visitor) => visitor.VisitVariableExpr(this);
        }

        public abstract R Accept<R>(IVisitor<R> visitor);
    }
}
