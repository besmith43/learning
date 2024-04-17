// This file contains helper functions that are automatically created from
// templates.

#include "nuitka/prelude.h"

extern PyObject *callPythonFunction( PyObject *func, PyObject **args, int count );


PyObject *CALL_FUNCTION_WITH_ARGS1( PyObject *called, PyObject **args )
{
    CHECK_OBJECT( called );

    // Check if arguments are valid objects in debug mode.
#ifndef __NUITKA_NO_ASSERT__
    for( size_t i = 0; i < 1; i++ )
    {
        CHECK_OBJECT( args[ i ] );
    }
#endif

    if ( Nuitka_Function_Check( called ) )
    {
        if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
        {
            return NULL;
        }

        struct Nuitka_FunctionObject *function = (struct Nuitka_FunctionObject *)called;
        PyObject *result;

        if ( function->m_args_simple && 1 == function->m_args_positional_count )
        {
            for( Py_ssize_t i = 0; i < 1; i++ )
            {
                Py_INCREF( args[ i ] );
            }

            result = function->m_c_code( function, args );
        }
        else if ( function->m_args_simple && 1 + function->m_defaults_given == function->m_args_positional_count )
        {
#ifdef _MSC_VER
            PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
            PyObject *python_pars[ function->m_args_positional_count ];
#endif
            memcpy( python_pars, args, 1 * sizeof(PyObject *) );
            memcpy( python_pars + 1, &PyTuple_GET_ITEM( function->m_defaults, 0 ), function->m_defaults_given * sizeof(PyObject *) );

            for( Py_ssize_t i = 0; i < function->m_args_positional_count; i++ )
            {
                Py_INCREF( python_pars[ i ] );
            }

            result = function->m_c_code( function, python_pars );
        }
        else
        {
#ifdef _MSC_VER
            PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_overall_count );
#else
            PyObject *python_pars[ function->m_args_overall_count ];
#endif
            memset( python_pars, 0, function->m_args_overall_count * sizeof(PyObject *) );

            if ( parseArgumentsPos( function, python_pars, args, 1 ))
            {
                result = function->m_c_code( function, python_pars );
            }
            else
            {
                result = NULL;
            }
        }

        Py_LeaveRecursiveCall();

        return result;
    }
    else if ( Nuitka_Method_Check( called ) )
    {
        struct Nuitka_MethodObject *method = (struct Nuitka_MethodObject *)called;

        // Unbound method without arguments, let the error path be slow.
        if ( method->m_object != NULL )
        {
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }

            struct Nuitka_FunctionObject *function = method->m_function;

            PyObject *result;

            if ( function->m_args_simple && 1 + 1 == function->m_args_positional_count )
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
                PyObject *python_pars[ function->m_args_positional_count ];
#endif
                python_pars[ 0 ] = method->m_object;
                Py_INCREF( method->m_object );

                for( Py_ssize_t i = 0; i < 1; i++ )
                {
                    python_pars[ i + 1 ] = args[ i ];
                    Py_INCREF( args[ i ] );
                }

                result = function->m_c_code( function, python_pars );
            }
            else if ( function->m_args_simple && 1 + 1 + function->m_defaults_given == function->m_args_positional_count )
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
                PyObject *python_pars[ function->m_args_positional_count ];
#endif
                python_pars[ 0 ] = method->m_object;
                Py_INCREF( method->m_object );

                memcpy( python_pars+1, args, 1 * sizeof(PyObject *) );
                memcpy( python_pars+1 + 1, &PyTuple_GET_ITEM( function->m_defaults, 0 ), function->m_defaults_given * sizeof(PyObject *) );

                for( Py_ssize_t i = 1; i < function->m_args_overall_count; i++ )
                {
                    Py_INCREF( python_pars[ i ] );
                }

                result = function->m_c_code( function, python_pars );
            }
            else
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_overall_count );
#else
                PyObject *python_pars[ function->m_args_overall_count ];
#endif
                memset( python_pars, 0, function->m_args_overall_count * sizeof(PyObject *) );

                if ( parseArgumentsMethodPos( function, python_pars, method->m_object, args, 1 ) )
                {
                    result = function->m_c_code( function, python_pars );
                }
                else
                {
                    result = NULL;
                }
            }

            Py_LeaveRecursiveCall();

            return result;
        }
    }
    else if ( PyCFunction_Check( called ) )
    {
        // Try to be fast about wrapping the arguments.
        int flags = PyCFunction_GET_FLAGS( called ) & ~(METH_CLASS | METH_STATIC | METH_COEXIST);

        if ( flags & METH_NOARGS )
        {
#if 1 == 0
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            PyObject *result = (*method)( self, NULL );

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                return NULL;
            }
#else
            PyErr_Format(
                PyExc_TypeError,
                "%s() takes no arguments (1 given)",
                ((PyCFunctionObject *)called)->m_ml->ml_name
            );
            return NULL;
#endif
        }
        else if ( flags & METH_O )
        {
#if 1 == 1
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            PyObject *result = (*method)( self, args[0] );

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                return NULL;
            }
#else
            PyErr_Format(PyExc_TypeError,
                "%s() takes exactly one argument (1 given)",
                 ((PyCFunctionObject *)called)->m_ml->ml_name
            );
            return NULL;
#endif
        }
        else
        {
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            PyObject *pos_args = MAKE_TUPLE( args, 1 );

            PyObject *result;

            assert( flags && METH_VARARGS );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

#if PYTHON_VERSION < 360
            if ( flags && METH_KEYWORDS )
            {
                result = (*(PyCFunctionWithKeywords)method)( self, pos_args, NULL );
            }
            else
            {
                result = (*method)( self, pos_args );
            }
#else
            if ( flags == ( METH_VARARGS | METH_KEYWORDS ) )
            {
                result = (*(PyCFunctionWithKeywords)method)( self, pos_args, NULL );
            }
            else if ( flags == METH_FASTCALL )
            {
                result = (*(_PyCFunctionFast)method)( self, &PyTuple_GET_ITEM( pos_args, 0 ), 1, NULL );;
            }
            else
            {
                result = (*method)( self, pos_args );
            }
#endif

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                Py_DECREF( pos_args );
                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                Py_DECREF( pos_args );
                return NULL;
            }
        }
    }
    else if ( PyFunction_Check( called ) )
    {
        return callPythonFunction(
            called,
            args,
            1
        );
    }

    PyObject *pos_args = MAKE_TUPLE( args, 1 );

    PyObject *result = CALL_FUNCTION(
        called,
        pos_args,
        NULL
    );

    Py_DECREF( pos_args );

    return result;
}

PyObject *CALL_FUNCTION_WITH_ARGS2( PyObject *called, PyObject **args )
{
    CHECK_OBJECT( called );

    // Check if arguments are valid objects in debug mode.
#ifndef __NUITKA_NO_ASSERT__
    for( size_t i = 0; i < 2; i++ )
    {
        CHECK_OBJECT( args[ i ] );
    }
#endif

    if ( Nuitka_Function_Check( called ) )
    {
        if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
        {
            return NULL;
        }

        struct Nuitka_FunctionObject *function = (struct Nuitka_FunctionObject *)called;
        PyObject *result;

        if ( function->m_args_simple && 2 == function->m_args_positional_count )
        {
            for( Py_ssize_t i = 0; i < 2; i++ )
            {
                Py_INCREF( args[ i ] );
            }

            result = function->m_c_code( function, args );
        }
        else if ( function->m_args_simple && 2 + function->m_defaults_given == function->m_args_positional_count )
        {
#ifdef _MSC_VER
            PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
            PyObject *python_pars[ function->m_args_positional_count ];
#endif
            memcpy( python_pars, args, 2 * sizeof(PyObject *) );
            memcpy( python_pars + 2, &PyTuple_GET_ITEM( function->m_defaults, 0 ), function->m_defaults_given * sizeof(PyObject *) );

            for( Py_ssize_t i = 0; i < function->m_args_positional_count; i++ )
            {
                Py_INCREF( python_pars[ i ] );
            }

            result = function->m_c_code( function, python_pars );
        }
        else
        {
#ifdef _MSC_VER
            PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_overall_count );
#else
            PyObject *python_pars[ function->m_args_overall_count ];
#endif
            memset( python_pars, 0, function->m_args_overall_count * sizeof(PyObject *) );

            if ( parseArgumentsPos( function, python_pars, args, 2 ))
            {
                result = function->m_c_code( function, python_pars );
            }
            else
            {
                result = NULL;
            }
        }

        Py_LeaveRecursiveCall();

        return result;
    }
    else if ( Nuitka_Method_Check( called ) )
    {
        struct Nuitka_MethodObject *method = (struct Nuitka_MethodObject *)called;

        // Unbound method without arguments, let the error path be slow.
        if ( method->m_object != NULL )
        {
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }

            struct Nuitka_FunctionObject *function = method->m_function;

            PyObject *result;

            if ( function->m_args_simple && 2 + 1 == function->m_args_positional_count )
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
                PyObject *python_pars[ function->m_args_positional_count ];
#endif
                python_pars[ 0 ] = method->m_object;
                Py_INCREF( method->m_object );

                for( Py_ssize_t i = 0; i < 2; i++ )
                {
                    python_pars[ i + 1 ] = args[ i ];
                    Py_INCREF( args[ i ] );
                }

                result = function->m_c_code( function, python_pars );
            }
            else if ( function->m_args_simple && 2 + 1 + function->m_defaults_given == function->m_args_positional_count )
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
                PyObject *python_pars[ function->m_args_positional_count ];
#endif
                python_pars[ 0 ] = method->m_object;
                Py_INCREF( method->m_object );

                memcpy( python_pars+1, args, 2 * sizeof(PyObject *) );
                memcpy( python_pars+1 + 2, &PyTuple_GET_ITEM( function->m_defaults, 0 ), function->m_defaults_given * sizeof(PyObject *) );

                for( Py_ssize_t i = 1; i < function->m_args_overall_count; i++ )
                {
                    Py_INCREF( python_pars[ i ] );
                }

                result = function->m_c_code( function, python_pars );
            }
            else
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_overall_count );
#else
                PyObject *python_pars[ function->m_args_overall_count ];
#endif
                memset( python_pars, 0, function->m_args_overall_count * sizeof(PyObject *) );

                if ( parseArgumentsMethodPos( function, python_pars, method->m_object, args, 2 ) )
                {
                    result = function->m_c_code( function, python_pars );
                }
                else
                {
                    result = NULL;
                }
            }

            Py_LeaveRecursiveCall();

            return result;
        }
    }
    else if ( PyCFunction_Check( called ) )
    {
        // Try to be fast about wrapping the arguments.
        int flags = PyCFunction_GET_FLAGS( called ) & ~(METH_CLASS | METH_STATIC | METH_COEXIST);

        if ( flags & METH_NOARGS )
        {
#if 2 == 0
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            PyObject *result = (*method)( self, NULL );

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                return NULL;
            }
#else
            PyErr_Format(
                PyExc_TypeError,
                "%s() takes no arguments (2 given)",
                ((PyCFunctionObject *)called)->m_ml->ml_name
            );
            return NULL;
#endif
        }
        else if ( flags & METH_O )
        {
#if 2 == 1
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            PyObject *result = (*method)( self, args[0] );

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                return NULL;
            }
#else
            PyErr_Format(PyExc_TypeError,
                "%s() takes exactly one argument (2 given)",
                 ((PyCFunctionObject *)called)->m_ml->ml_name
            );
            return NULL;
#endif
        }
        else
        {
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            PyObject *pos_args = MAKE_TUPLE( args, 2 );

            PyObject *result;

            assert( flags && METH_VARARGS );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

#if PYTHON_VERSION < 360
            if ( flags && METH_KEYWORDS )
            {
                result = (*(PyCFunctionWithKeywords)method)( self, pos_args, NULL );
            }
            else
            {
                result = (*method)( self, pos_args );
            }
#else
            if ( flags == ( METH_VARARGS | METH_KEYWORDS ) )
            {
                result = (*(PyCFunctionWithKeywords)method)( self, pos_args, NULL );
            }
            else if ( flags == METH_FASTCALL )
            {
                result = (*(_PyCFunctionFast)method)( self, &PyTuple_GET_ITEM( pos_args, 0 ), 2, NULL );;
            }
            else
            {
                result = (*method)( self, pos_args );
            }
#endif

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                Py_DECREF( pos_args );
                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                Py_DECREF( pos_args );
                return NULL;
            }
        }
    }
    else if ( PyFunction_Check( called ) )
    {
        return callPythonFunction(
            called,
            args,
            2
        );
    }

    PyObject *pos_args = MAKE_TUPLE( args, 2 );

    PyObject *result = CALL_FUNCTION(
        called,
        pos_args,
        NULL
    );

    Py_DECREF( pos_args );

    return result;
}

PyObject *CALL_FUNCTION_WITH_ARGS3( PyObject *called, PyObject **args )
{
    CHECK_OBJECT( called );

    // Check if arguments are valid objects in debug mode.
#ifndef __NUITKA_NO_ASSERT__
    for( size_t i = 0; i < 3; i++ )
    {
        CHECK_OBJECT( args[ i ] );
    }
#endif

    if ( Nuitka_Function_Check( called ) )
    {
        if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
        {
            return NULL;
        }

        struct Nuitka_FunctionObject *function = (struct Nuitka_FunctionObject *)called;
        PyObject *result;

        if ( function->m_args_simple && 3 == function->m_args_positional_count )
        {
            for( Py_ssize_t i = 0; i < 3; i++ )
            {
                Py_INCREF( args[ i ] );
            }

            result = function->m_c_code( function, args );
        }
        else if ( function->m_args_simple && 3 + function->m_defaults_given == function->m_args_positional_count )
        {
#ifdef _MSC_VER
            PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
            PyObject *python_pars[ function->m_args_positional_count ];
#endif
            memcpy( python_pars, args, 3 * sizeof(PyObject *) );
            memcpy( python_pars + 3, &PyTuple_GET_ITEM( function->m_defaults, 0 ), function->m_defaults_given * sizeof(PyObject *) );

            for( Py_ssize_t i = 0; i < function->m_args_positional_count; i++ )
            {
                Py_INCREF( python_pars[ i ] );
            }

            result = function->m_c_code( function, python_pars );
        }
        else
        {
#ifdef _MSC_VER
            PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_overall_count );
#else
            PyObject *python_pars[ function->m_args_overall_count ];
#endif
            memset( python_pars, 0, function->m_args_overall_count * sizeof(PyObject *) );

            if ( parseArgumentsPos( function, python_pars, args, 3 ))
            {
                result = function->m_c_code( function, python_pars );
            }
            else
            {
                result = NULL;
            }
        }

        Py_LeaveRecursiveCall();

        return result;
    }
    else if ( Nuitka_Method_Check( called ) )
    {
        struct Nuitka_MethodObject *method = (struct Nuitka_MethodObject *)called;

        // Unbound method without arguments, let the error path be slow.
        if ( method->m_object != NULL )
        {
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }

            struct Nuitka_FunctionObject *function = method->m_function;

            PyObject *result;

            if ( function->m_args_simple && 3 + 1 == function->m_args_positional_count )
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
                PyObject *python_pars[ function->m_args_positional_count ];
#endif
                python_pars[ 0 ] = method->m_object;
                Py_INCREF( method->m_object );

                for( Py_ssize_t i = 0; i < 3; i++ )
                {
                    python_pars[ i + 1 ] = args[ i ];
                    Py_INCREF( args[ i ] );
                }

                result = function->m_c_code( function, python_pars );
            }
            else if ( function->m_args_simple && 3 + 1 + function->m_defaults_given == function->m_args_positional_count )
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_positional_count );
#else
                PyObject *python_pars[ function->m_args_positional_count ];
#endif
                python_pars[ 0 ] = method->m_object;
                Py_INCREF( method->m_object );

                memcpy( python_pars+1, args, 3 * sizeof(PyObject *) );
                memcpy( python_pars+1 + 3, &PyTuple_GET_ITEM( function->m_defaults, 0 ), function->m_defaults_given * sizeof(PyObject *) );

                for( Py_ssize_t i = 1; i < function->m_args_overall_count; i++ )
                {
                    Py_INCREF( python_pars[ i ] );
                }

                result = function->m_c_code( function, python_pars );
            }
            else
            {
#ifdef _MSC_VER
                PyObject **python_pars = (PyObject **)_alloca( sizeof( PyObject * ) * function->m_args_overall_count );
#else
                PyObject *python_pars[ function->m_args_overall_count ];
#endif
                memset( python_pars, 0, function->m_args_overall_count * sizeof(PyObject *) );

                if ( parseArgumentsMethodPos( function, python_pars, method->m_object, args, 3 ) )
                {
                    result = function->m_c_code( function, python_pars );
                }
                else
                {
                    result = NULL;
                }
            }

            Py_LeaveRecursiveCall();

            return result;
        }
    }
    else if ( PyCFunction_Check( called ) )
    {
        // Try to be fast about wrapping the arguments.
        int flags = PyCFunction_GET_FLAGS( called ) & ~(METH_CLASS | METH_STATIC | METH_COEXIST);

        if ( flags & METH_NOARGS )
        {
#if 3 == 0
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            PyObject *result = (*method)( self, NULL );

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                return NULL;
            }
#else
            PyErr_Format(
                PyExc_TypeError,
                "%s() takes no arguments (3 given)",
                ((PyCFunctionObject *)called)->m_ml->ml_name
            );
            return NULL;
#endif
        }
        else if ( flags & METH_O )
        {
#if 3 == 1
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

            PyObject *result = (*method)( self, args[0] );

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                return NULL;
            }
#else
            PyErr_Format(PyExc_TypeError,
                "%s() takes exactly one argument (3 given)",
                 ((PyCFunctionObject *)called)->m_ml->ml_name
            );
            return NULL;
#endif
        }
        else
        {
            PyCFunction method = PyCFunction_GET_FUNCTION( called );
            PyObject *self = PyCFunction_GET_SELF( called );

            PyObject *pos_args = MAKE_TUPLE( args, 3 );

            PyObject *result;

            assert( flags && METH_VARARGS );

            // Recursion guard is not strictly necessary, as we already have
            // one on our way to here.
#ifdef _NUITKA_FULL_COMPAT
            if (unlikely( Py_EnterRecursiveCall( (char *)" while calling a Python object" ) ))
            {
                return NULL;
            }
#endif

#if PYTHON_VERSION < 360
            if ( flags && METH_KEYWORDS )
            {
                result = (*(PyCFunctionWithKeywords)method)( self, pos_args, NULL );
            }
            else
            {
                result = (*method)( self, pos_args );
            }
#else
            if ( flags == ( METH_VARARGS | METH_KEYWORDS ) )
            {
                result = (*(PyCFunctionWithKeywords)method)( self, pos_args, NULL );
            }
            else if ( flags == METH_FASTCALL )
            {
                result = (*(_PyCFunctionFast)method)( self, &PyTuple_GET_ITEM( pos_args, 0 ), 3, NULL );;
            }
            else
            {
                result = (*method)( self, pos_args );
            }
#endif

#ifdef _NUITKA_FULL_COMPAT
            Py_LeaveRecursiveCall();
#endif

            if ( result != NULL )
            {
            // Some buggy C functions do set an error, but do not indicate it
            // and Nuitka inner workings can get upset/confused from it.
                DROP_ERROR_OCCURRED();

                Py_DECREF( pos_args );
                return result;
            }
            else
            {
                // Other buggy C functions do this, return NULL, but with
                // no error set, not allowed.
                if (unlikely( !ERROR_OCCURRED() ))
                {
                    PyErr_Format(
                        PyExc_SystemError,
                        "NULL result without error in PyObject_Call"
                    );
                }

                Py_DECREF( pos_args );
                return NULL;
            }
        }
    }
    else if ( PyFunction_Check( called ) )
    {
        return callPythonFunction(
            called,
            args,
            3
        );
    }

    PyObject *pos_args = MAKE_TUPLE( args, 3 );

    PyObject *result = CALL_FUNCTION(
        called,
        pos_args,
        NULL
    );

    Py_DECREF( pos_args );

    return result;
}
/* Code to register embedded modules for meta path based loading if any. */

#include "nuitka/unfreezing.h"

/* Table for lookup to find compiled or bytecode modules included in this
 * binary or module, or put along this binary as extension modules. We do
 * our own loading for each of these.
 */
MOD_INIT_DECL( __main__ );
static struct Nuitka_MetaPathBasedLoaderEntry meta_path_loader_entries[] =
{
    { (char *)"__main__", MOD_INIT_NAME( __main__ ), 0, 0, NUITKA_COMPILED_MODULE },
    { (char *)"_bsddb", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_codecs_cn", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_codecs_hk", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_codecs_iso2022", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_codecs_jp", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_codecs_kr", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_codecs_tw", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_csv", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_ctypes", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_curses", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_curses_panel", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_elementtree", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_hashlib", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_hotshot", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_json", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_lsprof", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_multibytecodec", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_multiprocessing", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_sqlite3", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"_ssl", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"bz2", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"dbm", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"mmap", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"parser", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"pyexpat", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"readline", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"resource", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"termios", NULL, 0, 0, NUITKA_SHLIB_FLAG },
    { (char *)"BaseHTTPServer", NULL, 346, 21674, NUITKA_BYTECODE_FLAG },
    { (char *)"Bastion", NULL, 22020, 6622, NUITKA_BYTECODE_FLAG },
    { (char *)"CGIHTTPServer", NULL, 28642, 10984, NUITKA_BYTECODE_FLAG },
    { (char *)"Canvas", NULL, 39626, 15396, NUITKA_BYTECODE_FLAG },
    { (char *)"ConfigParser", NULL, 55022, 25087, NUITKA_BYTECODE_FLAG },
    { (char *)"Cookie", NULL, 80109, 22574, NUITKA_BYTECODE_FLAG },
    { (char *)"Dialog", NULL, 102683, 1902, NUITKA_BYTECODE_FLAG },
    { (char *)"DocXMLRPCServer", NULL, 104585, 9791, NUITKA_BYTECODE_FLAG },
    { (char *)"FileDialog", NULL, 114376, 9666, NUITKA_BYTECODE_FLAG },
    { (char *)"FixTk", NULL, 124042, 2078, NUITKA_BYTECODE_FLAG },
    { (char *)"HTMLParser", NULL, 126120, 13651, NUITKA_BYTECODE_FLAG },
    { (char *)"MimeWriter", NULL, 139771, 7338, NUITKA_BYTECODE_FLAG },
    { (char *)"Queue", NULL, 147109, 9360, NUITKA_BYTECODE_FLAG },
    { (char *)"ScrolledText", NULL, 156469, 2646, NUITKA_BYTECODE_FLAG },
    { (char *)"SimpleDialog", NULL, 159115, 4309, NUITKA_BYTECODE_FLAG },
    { (char *)"SimpleHTTPServer", NULL, 163424, 7980, NUITKA_BYTECODE_FLAG },
    { (char *)"SimpleXMLRPCServer", NULL, 171404, 22783, NUITKA_BYTECODE_FLAG },
    { (char *)"SocketServer", NULL, 194187, 23948, NUITKA_BYTECODE_FLAG },
    { (char *)"StringIO", NULL, 218135, 11434, NUITKA_BYTECODE_FLAG },
    { (char *)"Tix", NULL, 229569, 95416, NUITKA_BYTECODE_FLAG },
    { (char *)"Tkconstants", NULL, 324985, 2236, NUITKA_BYTECODE_FLAG },
    { (char *)"Tkdnd", NULL, 327221, 12765, NUITKA_BYTECODE_FLAG },
    { (char *)"Tkinter", NULL, 339986, 198990, NUITKA_BYTECODE_FLAG },
    { (char *)"UserDict", NULL, 538976, 9613, NUITKA_BYTECODE_FLAG },
    { (char *)"UserList", NULL, 548589, 6501, NUITKA_BYTECODE_FLAG },
    { (char *)"UserString", NULL, 555090, 14720, NUITKA_BYTECODE_FLAG },
    { (char *)"_LWPCookieJar", NULL, 569810, 5414, NUITKA_BYTECODE_FLAG },
    { (char *)"_MozillaCookieJar", NULL, 575224, 4445, NUITKA_BYTECODE_FLAG },
    { (char *)"__future__", NULL, 579669, 4203, NUITKA_BYTECODE_FLAG },
    { (char *)"_abcoll", NULL, 583872, 25466, NUITKA_BYTECODE_FLAG },
    { (char *)"_osx_support", NULL, 609338, 11712, NUITKA_BYTECODE_FLAG },
    { (char *)"_pyio", NULL, 621050, 64318, NUITKA_BYTECODE_FLAG },
    { (char *)"_strptime", NULL, 685368, 15115, NUITKA_BYTECODE_FLAG },
    { (char *)"_sysconfigdata", NULL, 700483, 271, NUITKA_BYTECODE_FLAG },
    { (char *)"_sysconfigdata_nd", NULL, 700754, 20826, NUITKA_BYTECODE_FLAG },
    { (char *)"_threading_local", NULL, 721580, 6543, NUITKA_BYTECODE_FLAG },
    { (char *)"_weakrefset", NULL, 728123, 9574, NUITKA_BYTECODE_FLAG },
    { (char *)"abc", NULL, 737697, 6113, NUITKA_BYTECODE_FLAG },
    { (char *)"aifc", NULL, 743810, 30199, NUITKA_BYTECODE_FLAG },
    { (char *)"anydbm", NULL, 774009, 2786, NUITKA_BYTECODE_FLAG },
    { (char *)"argparse", NULL, 776795, 64033, NUITKA_BYTECODE_FLAG },
    { (char *)"ast", NULL, 840828, 12882, NUITKA_BYTECODE_FLAG },
    { (char *)"asynchat", NULL, 853710, 8751, NUITKA_BYTECODE_FLAG },
    { (char *)"asyncore", NULL, 862461, 18710, NUITKA_BYTECODE_FLAG },
    { (char *)"atexit", NULL, 881171, 2183, NUITKA_BYTECODE_FLAG },
    { (char *)"audiodev", NULL, 883354, 8407, NUITKA_BYTECODE_FLAG },
    { (char *)"bdb", NULL, 891761, 18971, NUITKA_BYTECODE_FLAG },
    { (char *)"binhex", NULL, 910732, 15350, NUITKA_BYTECODE_FLAG },
    { (char *)"bisect", NULL, 926082, 3053, NUITKA_BYTECODE_FLAG },
    { (char *)"bsddb", NULL, 929135, 12368, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"bsddb.db", NULL, 941503, 582, NUITKA_BYTECODE_FLAG },
    { (char *)"bsddb.dbobj", NULL, 942085, 18601, NUITKA_BYTECODE_FLAG },
    { (char *)"bsddb.dbrecio", NULL, 960686, 5269, NUITKA_BYTECODE_FLAG },
    { (char *)"bsddb.dbshelve", NULL, 965955, 12910, NUITKA_BYTECODE_FLAG },
    { (char *)"bsddb.dbtables", NULL, 978865, 24402, NUITKA_BYTECODE_FLAG },
    { (char *)"bsddb.dbutils", NULL, 1003267, 1615, NUITKA_BYTECODE_FLAG },
    { (char *)"cProfile", NULL, 1004882, 6272, NUITKA_BYTECODE_FLAG },
    { (char *)"calendar", NULL, 1011154, 27747, NUITKA_BYTECODE_FLAG },
    { (char *)"cgi", NULL, 1038901, 32417, NUITKA_BYTECODE_FLAG },
    { (char *)"cgitb", NULL, 1071318, 12127, NUITKA_BYTECODE_FLAG },
    { (char *)"chunk", NULL, 1083445, 5572, NUITKA_BYTECODE_FLAG },
    { (char *)"cmd", NULL, 1089017, 13989, NUITKA_BYTECODE_FLAG },
    { (char *)"code", NULL, 1103006, 10294, NUITKA_BYTECODE_FLAG },
    { (char *)"codeop", NULL, 1113300, 6569, NUITKA_BYTECODE_FLAG },
    { (char *)"collections", NULL, 1119869, 26051, NUITKA_BYTECODE_FLAG },
    { (char *)"colorsys", NULL, 1145920, 3967, NUITKA_BYTECODE_FLAG },
    { (char *)"commands", NULL, 1149887, 2449, NUITKA_BYTECODE_FLAG },
    { (char *)"compileall", NULL, 1152336, 6997, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler", NULL, 1159333, 1287, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"compiler.ast", NULL, 1160620, 71339, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.consts", NULL, 1231959, 727, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.future", NULL, 1232686, 2928, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.misc", NULL, 1235614, 3687, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.pyassem", NULL, 1239301, 25768, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.pycodegen", NULL, 1265069, 56161, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.symbols", NULL, 1321230, 17557, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.syntax", NULL, 1338787, 1862, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.transformer", NULL, 1340649, 47372, NUITKA_BYTECODE_FLAG },
    { (char *)"compiler.visitor", NULL, 1388021, 4159, NUITKA_BYTECODE_FLAG },
    { (char *)"contextlib", NULL, 1392180, 4422, NUITKA_BYTECODE_FLAG },
    { (char *)"cookielib", NULL, 1396602, 54248, NUITKA_BYTECODE_FLAG },
    { (char *)"copy", NULL, 1450850, 12110, NUITKA_BYTECODE_FLAG },
    { (char *)"csv", NULL, 1462960, 13443, NUITKA_BYTECODE_FLAG },
    { (char *)"ctypes", NULL, 1476403, 20160, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"ctypes._endian", NULL, 1496563, 2287, NUITKA_BYTECODE_FLAG },
    { (char *)"ctypes.util", NULL, 1498850, 8518, NUITKA_BYTECODE_FLAG },
    { (char *)"curses", NULL, 1507368, 1539, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"curses.ascii", NULL, 1508907, 5052, NUITKA_BYTECODE_FLAG },
    { (char *)"curses.has_key", NULL, 1513959, 5925, NUITKA_BYTECODE_FLAG },
    { (char *)"curses.panel", NULL, 1519884, 267, NUITKA_BYTECODE_FLAG },
    { (char *)"curses.textpad", NULL, 1520151, 6842, NUITKA_BYTECODE_FLAG },
    { (char *)"curses.wrapper", NULL, 1526993, 1206, NUITKA_BYTECODE_FLAG },
    { (char *)"dbhash", NULL, 1528199, 706, NUITKA_BYTECODE_FLAG },
    { (char *)"decimal", NULL, 1528905, 171637, NUITKA_BYTECODE_FLAG },
    { (char *)"difflib", NULL, 1700542, 61782, NUITKA_BYTECODE_FLAG },
    { (char *)"dircache", NULL, 1762324, 1560, NUITKA_BYTECODE_FLAG },
    { (char *)"dis", NULL, 1763884, 6204, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils", NULL, 1770088, 405, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"distutils.archive_util", NULL, 1770493, 7383, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.bcppcompiler", NULL, 1777876, 7856, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.ccompiler", NULL, 1785732, 36736, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.cmd", NULL, 1822468, 16722, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command", NULL, 1839190, 655, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"distutils.command.bdist", NULL, 1839845, 5149, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.bdist_dumb", NULL, 1844994, 4963, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.bdist_msi", NULL, 1849957, 23917, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.bdist_rpm", NULL, 1873874, 17580, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.bdist_wininst", NULL, 1891454, 10878, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.build", NULL, 1902332, 5115, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.build_clib", NULL, 1907447, 6319, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.build_ext", NULL, 1913766, 19252, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.build_py", NULL, 1933018, 11443, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.build_scripts", NULL, 1944461, 4455, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.check", NULL, 1948916, 6192, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.clean", NULL, 1955108, 3051, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.config", NULL, 1958159, 12637, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.install", NULL, 1970796, 17884, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.install_data", NULL, 1988680, 3096, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.install_egg_info", NULL, 1991776, 4345, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.install_headers", NULL, 1996121, 2233, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.install_lib", NULL, 1998354, 6649, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.install_scripts", NULL, 2005003, 2906, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.register", NULL, 2007909, 10174, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.sdist", NULL, 2018083, 16652, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.command.upload", NULL, 2034735, 6295, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.config", NULL, 2041030, 3543, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.core", NULL, 2044573, 7520, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.cygwinccompiler", NULL, 2052093, 9793, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.debug", NULL, 2061886, 244, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.dep_util", NULL, 2062130, 3164, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.dir_util", NULL, 2065294, 6764, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.dist", NULL, 2072058, 39034, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.emxccompiler", NULL, 2111092, 7441, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.errors", NULL, 2118533, 6237, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.extension", NULL, 2124770, 7396, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.fancy_getopt", NULL, 2132166, 11908, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.file_util", NULL, 2144074, 6732, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.filelist", NULL, 2150806, 10714, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.log", NULL, 2161520, 2754, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.msvc9compiler", NULL, 2164274, 21427, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.msvccompiler", NULL, 2185701, 17465, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.spawn", NULL, 2203166, 6377, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.sysconfig", NULL, 2209543, 15079, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.text_file", NULL, 2224622, 9229, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.unixccompiler", NULL, 2233851, 8087, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.util", NULL, 2241938, 14304, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.version", NULL, 2256242, 7170, NUITKA_BYTECODE_FLAG },
    { (char *)"distutils.versionpredicate", NULL, 2263412, 5520, NUITKA_BYTECODE_FLAG },
    { (char *)"doctest", NULL, 2268932, 83440, NUITKA_BYTECODE_FLAG },
    { (char *)"dumbdbm", NULL, 2352372, 6700, NUITKA_BYTECODE_FLAG },
    { (char *)"dummy_thread", NULL, 2359072, 5356, NUITKA_BYTECODE_FLAG },
    { (char *)"dummy_threading", NULL, 2364428, 1275, NUITKA_BYTECODE_FLAG },
    { (char *)"email", NULL, 2365703, 2852, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"email._parseaddr", NULL, 2368555, 13763, NUITKA_BYTECODE_FLAG },
    { (char *)"email.base64mime", NULL, 2382318, 5305, NUITKA_BYTECODE_FLAG },
    { (char *)"email.charset", NULL, 2387623, 13499, NUITKA_BYTECODE_FLAG },
    { (char *)"email.encoders", NULL, 2401122, 2210, NUITKA_BYTECODE_FLAG },
    { (char *)"email.errors", NULL, 2403332, 3491, NUITKA_BYTECODE_FLAG },
    { (char *)"email.feedparser", NULL, 2406823, 11516, NUITKA_BYTECODE_FLAG },
    { (char *)"email.generator", NULL, 2418339, 10334, NUITKA_BYTECODE_FLAG },
    { (char *)"email.header", NULL, 2428673, 13622, NUITKA_BYTECODE_FLAG },
    { (char *)"email.iterators", NULL, 2442295, 2348, NUITKA_BYTECODE_FLAG },
    { (char *)"email.message", NULL, 2444643, 28576, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime", NULL, 2473219, 120, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"email.mime.application", NULL, 2473339, 1570, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.audio", NULL, 2474909, 2893, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.base", NULL, 2477802, 1102, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.image", NULL, 2478904, 2035, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.message", NULL, 2480939, 1434, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.multipart", NULL, 2482373, 1655, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.nonmultipart", NULL, 2484028, 874, NUITKA_BYTECODE_FLAG },
    { (char *)"email.mime.text", NULL, 2484902, 1294, NUITKA_BYTECODE_FLAG },
    { (char *)"email.parser", NULL, 2486196, 3804, NUITKA_BYTECODE_FLAG },
    { (char *)"email.quoprimime", NULL, 2490000, 8816, NUITKA_BYTECODE_FLAG },
    { (char *)"email.utils", NULL, 2498816, 9082, NUITKA_BYTECODE_FLAG },
    { (char *)"encodings.mbcs", NULL, 2507898, 2019, NUITKA_BYTECODE_FLAG },
    { (char *)"filecmp", NULL, 2509917, 9574, NUITKA_BYTECODE_FLAG },
    { (char *)"fileinput", NULL, 2519491, 14432, NUITKA_BYTECODE_FLAG },
    { (char *)"fnmatch", NULL, 2533923, 3594, NUITKA_BYTECODE_FLAG },
    { (char *)"formatter", NULL, 2537517, 19016, NUITKA_BYTECODE_FLAG },
    { (char *)"fpformat", NULL, 2556533, 4648, NUITKA_BYTECODE_FLAG },
    { (char *)"fractions", NULL, 2561181, 19625, NUITKA_BYTECODE_FLAG },
    { (char *)"ftplib", NULL, 2580806, 34430, NUITKA_BYTECODE_FLAG },
    { (char *)"genericpath", NULL, 2615236, 3487, NUITKA_BYTECODE_FLAG },
    { (char *)"getopt", NULL, 2618723, 6626, NUITKA_BYTECODE_FLAG },
    { (char *)"getpass", NULL, 2625349, 4722, NUITKA_BYTECODE_FLAG },
    { (char *)"gettext", NULL, 2630071, 17858, NUITKA_BYTECODE_FLAG },
    { (char *)"glob", NULL, 2647929, 2919, NUITKA_BYTECODE_FLAG },
    { (char *)"gzip", NULL, 2650848, 15129, NUITKA_BYTECODE_FLAG },
    { (char *)"hashlib", NULL, 2665977, 6883, NUITKA_BYTECODE_FLAG },
    { (char *)"heapq", NULL, 2672860, 14520, NUITKA_BYTECODE_FLAG },
    { (char *)"hmac", NULL, 2687380, 4514, NUITKA_BYTECODE_FLAG },
    { (char *)"hotshot", NULL, 2691894, 3454, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"hotshot.log", NULL, 2695348, 5507, NUITKA_BYTECODE_FLAG },
    { (char *)"hotshot.stats", NULL, 2700855, 3374, NUITKA_BYTECODE_FLAG },
    { (char *)"hotshot.stones", NULL, 2704229, 1144, NUITKA_BYTECODE_FLAG },
    { (char *)"htmlentitydefs", NULL, 2705373, 6357, NUITKA_BYTECODE_FLAG },
    { (char *)"htmllib", NULL, 2711730, 20119, NUITKA_BYTECODE_FLAG },
    { (char *)"httplib", NULL, 2731849, 36489, NUITKA_BYTECODE_FLAG },
    { (char *)"ihooks", NULL, 2768338, 21226, NUITKA_BYTECODE_FLAG },
    { (char *)"imaplib", NULL, 2789564, 45117, NUITKA_BYTECODE_FLAG },
    { (char *)"imghdr", NULL, 2834681, 4798, NUITKA_BYTECODE_FLAG },
    { (char *)"importlib", NULL, 2839479, 1488, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"imputil", NULL, 2840967, 15539, NUITKA_BYTECODE_FLAG },
    { (char *)"inspect", NULL, 2856506, 39899, NUITKA_BYTECODE_FLAG },
    { (char *)"io", NULL, 2896405, 3571, NUITKA_BYTECODE_FLAG },
    { (char *)"json", NULL, 2899976, 13906, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"json.decoder", NULL, 2913882, 11928, NUITKA_BYTECODE_FLAG },
    { (char *)"json.encoder", NULL, 2925810, 13679, NUITKA_BYTECODE_FLAG },
    { (char *)"json.scanner", NULL, 2939489, 2215, NUITKA_BYTECODE_FLAG },
    { (char *)"json.tool", NULL, 2941704, 1282, NUITKA_BYTECODE_FLAG },
    { (char *)"keyword", NULL, 2942986, 2093, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3", NULL, 2945079, 117, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"lib2to3.btm_matcher", NULL, 2945196, 5800, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.btm_utils", NULL, 2950996, 7529, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixer_base", NULL, 2958525, 7216, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixer_util", NULL, 2965741, 14607, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes", NULL, 2980348, 123, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"lib2to3.fixes.fix_apply", NULL, 2980471, 2070, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_asserts", NULL, 2982541, 1547, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_basestring", NULL, 2984088, 793, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_buffer", NULL, 2984881, 950, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_dict", NULL, 2985831, 3753, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_except", NULL, 2989584, 2993, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_exec", NULL, 2992577, 1418, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_execfile", NULL, 2993995, 2059, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_exitfunc", NULL, 2996054, 2739, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_filter", NULL, 2998793, 2256, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_funcattrs", NULL, 3001049, 1114, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_future", NULL, 3002163, 919, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_getcwdu", NULL, 3003082, 926, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_has_key", NULL, 3004008, 3184, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_idioms", NULL, 3007192, 4515, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_import", NULL, 3011707, 3233, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_imports", NULL, 3014940, 5352, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_imports2", NULL, 3020292, 622, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_input", NULL, 3020914, 1134, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_intern", NULL, 3022048, 1783, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_isinstance", NULL, 3023831, 1838, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_itertools", NULL, 3025669, 1791, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_itertools_imports", NULL, 3027460, 2016, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_long", NULL, 3029476, 841, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_map", NULL, 3030317, 3040, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_metaclass", NULL, 3033357, 6579, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_methodattrs", NULL, 3039936, 1138, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_ne", NULL, 3041074, 985, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_next", NULL, 3042059, 3531, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_nonzero", NULL, 3045590, 1086, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_numliterals", NULL, 3046676, 1249, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_operator", NULL, 3047925, 5112, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_paren", NULL, 3053037, 1543, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_print", NULL, 3054580, 2727, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_raise", NULL, 3057307, 2498, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_raw_input", NULL, 3059805, 936, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_reduce", NULL, 3060741, 1262, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_renames", NULL, 3062003, 2449, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_repr", NULL, 3064452, 1016, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_set_literal", NULL, 3065468, 1988, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_standarderror", NULL, 3067456, 853, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_sys_exc", NULL, 3068309, 1705, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_throw", NULL, 3070014, 1996, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_tuple_params", NULL, 3072010, 5430, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_types", NULL, 3077440, 2196, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_unicode", NULL, 3079636, 1716, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_urllib", NULL, 3081352, 7134, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_ws_comma", NULL, 3088486, 1382, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_xrange", NULL, 3089868, 3063, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_xreadlines", NULL, 3092931, 1152, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.fixes.fix_zip", NULL, 3094083, 1346, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.main", NULL, 3095429, 9803, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.patcomp", NULL, 3105232, 6634, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2", NULL, 3111866, 164, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"lib2to3.pgen2.conv", NULL, 3112030, 8169, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.driver", NULL, 3120199, 5528, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.grammar", NULL, 3125727, 7403, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.literals", NULL, 3133130, 2002, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.parse", NULL, 3135132, 7204, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.pgen", NULL, 3142336, 12151, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.token", NULL, 3154487, 2287, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pgen2.tokenize", NULL, 3156774, 16923, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pygram", NULL, 3173697, 1400, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.pytree", NULL, 3175097, 30143, NUITKA_BYTECODE_FLAG },
    { (char *)"lib2to3.refactor", NULL, 3205240, 23866, NUITKA_BYTECODE_FLAG },
    { (char *)"linecache", NULL, 3229106, 3252, NUITKA_BYTECODE_FLAG },
    { (char *)"logging", NULL, 3232358, 57286, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"logging.config", NULL, 3289644, 25834, NUITKA_BYTECODE_FLAG },
    { (char *)"logging.handlers", NULL, 3315478, 39149, NUITKA_BYTECODE_FLAG },
    { (char *)"macpath", NULL, 3354627, 7635, NUITKA_BYTECODE_FLAG },
    { (char *)"macurl2path", NULL, 3362262, 2228, NUITKA_BYTECODE_FLAG },
    { (char *)"mailbox", NULL, 3364490, 76271, NUITKA_BYTECODE_FLAG },
    { (char *)"mailcap", NULL, 3440761, 7045, NUITKA_BYTECODE_FLAG },
    { (char *)"markupbase", NULL, 3447806, 9225, NUITKA_BYTECODE_FLAG },
    { (char *)"md5", NULL, 3457031, 368, NUITKA_BYTECODE_FLAG },
    { (char *)"mhlib", NULL, 3457399, 33613, NUITKA_BYTECODE_FLAG },
    { (char *)"mimetools", NULL, 3491012, 8153, NUITKA_BYTECODE_FLAG },
    { (char *)"mimetypes", NULL, 3499165, 18385, NUITKA_BYTECODE_FLAG },
    { (char *)"mimify", NULL, 3517550, 11963, NUITKA_BYTECODE_FLAG },
    { (char *)"modulefinder", NULL, 3529513, 19049, NUITKA_BYTECODE_FLAG },
    { (char *)"multifile", NULL, 3548562, 5382, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing", NULL, 3553944, 8405, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"multiprocessing.connection", NULL, 3562349, 14310, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.dummy", NULL, 3576659, 5412, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"multiprocessing.dummy.connection", NULL, 3582071, 2732, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.forking", NULL, 3584803, 14336, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.heap", NULL, 3599139, 6861, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.managers", NULL, 3606000, 38119, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.pool", NULL, 3644119, 22367, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.process", NULL, 3666486, 9435, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.queues", NULL, 3675921, 11466, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.reduction", NULL, 3687387, 5972, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.sharedctypes", NULL, 3693359, 8572, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.synchronize", NULL, 3701931, 10939, NUITKA_BYTECODE_FLAG },
    { (char *)"multiprocessing.util", NULL, 3712870, 10066, NUITKA_BYTECODE_FLAG },
    { (char *)"mutex", NULL, 3722936, 2494, NUITKA_BYTECODE_FLAG },
    { (char *)"netrc", NULL, 3725430, 4646, NUITKA_BYTECODE_FLAG },
    { (char *)"new", NULL, 3730076, 852, NUITKA_BYTECODE_FLAG },
    { (char *)"nntplib", NULL, 3730928, 20948, NUITKA_BYTECODE_FLAG },
    { (char *)"ntpath", NULL, 3751876, 13077, NUITKA_BYTECODE_FLAG },
    { (char *)"nturl2path", NULL, 3764953, 1801, NUITKA_BYTECODE_FLAG },
    { (char *)"numbers", NULL, 3766754, 13880, NUITKA_BYTECODE_FLAG },
    { (char *)"opcode", NULL, 3780634, 6127, NUITKA_BYTECODE_FLAG },
    { (char *)"optparse", NULL, 3786761, 53606, NUITKA_BYTECODE_FLAG },
    { (char *)"os", NULL, 3840367, 25575, NUITKA_BYTECODE_FLAG },
    { (char *)"os2emxpath", NULL, 3865942, 4499, NUITKA_BYTECODE_FLAG },
    { (char *)"pdb", NULL, 3870441, 43433, NUITKA_BYTECODE_FLAG },
    { (char *)"pickle", NULL, 3913874, 38344, NUITKA_BYTECODE_FLAG },
    { (char *)"pickletools", NULL, 3952218, 56964, NUITKA_BYTECODE_FLAG },
    { (char *)"pipes", NULL, 4009182, 9268, NUITKA_BYTECODE_FLAG },
    { (char *)"pkgutil", NULL, 4018450, 18859, NUITKA_BYTECODE_FLAG },
    { (char *)"platform", NULL, 4037309, 37712, NUITKA_BYTECODE_FLAG },
    { (char *)"plistlib", NULL, 4075021, 19115, NUITKA_BYTECODE_FLAG },
    { (char *)"popen2", NULL, 4094136, 8985, NUITKA_BYTECODE_FLAG },
    { (char *)"poplib", NULL, 4103121, 13271, NUITKA_BYTECODE_FLAG },
    { (char *)"posixfile", NULL, 4116392, 7620, NUITKA_BYTECODE_FLAG },
    { (char *)"posixpath", NULL, 4124012, 11377, NUITKA_BYTECODE_FLAG },
    { (char *)"pprint", NULL, 4135389, 10148, NUITKA_BYTECODE_FLAG },
    { (char *)"profile", NULL, 4145537, 16370, NUITKA_BYTECODE_FLAG },
    { (char *)"pstats", NULL, 4161907, 24885, NUITKA_BYTECODE_FLAG },
    { (char *)"pty", NULL, 4186792, 4938, NUITKA_BYTECODE_FLAG },
    { (char *)"py_compile", NULL, 4191730, 6603, NUITKA_BYTECODE_FLAG },
    { (char *)"pyclbr", NULL, 4198333, 9617, NUITKA_BYTECODE_FLAG },
    { (char *)"pydoc", NULL, 4207950, 92057, NUITKA_BYTECODE_FLAG },
    { (char *)"pydoc_data", NULL, 4300007, 120, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"pydoc_data.topics", NULL, 4300127, 413593, NUITKA_BYTECODE_FLAG },
    { (char *)"random", NULL, 4713720, 25476, NUITKA_BYTECODE_FLAG },
    { (char *)"repr", NULL, 4739196, 5343, NUITKA_BYTECODE_FLAG },
    { (char *)"rexec", NULL, 4744539, 23681, NUITKA_BYTECODE_FLAG },
    { (char *)"rfc822", NULL, 4768220, 31685, NUITKA_BYTECODE_FLAG },
    { (char *)"rlcompleter", NULL, 4799905, 6054, NUITKA_BYTECODE_FLAG },
    { (char *)"robotparser", NULL, 4805959, 7904, NUITKA_BYTECODE_FLAG },
    { (char *)"runpy", NULL, 4813863, 8755, NUITKA_BYTECODE_FLAG },
    { (char *)"sched", NULL, 4822618, 4968, NUITKA_BYTECODE_FLAG },
    { (char *)"sets", NULL, 4827586, 16775, NUITKA_BYTECODE_FLAG },
    { (char *)"sgmllib", NULL, 4844361, 15334, NUITKA_BYTECODE_FLAG },
    { (char *)"sha", NULL, 4859695, 411, NUITKA_BYTECODE_FLAG },
    { (char *)"shelve", NULL, 4860106, 10194, NUITKA_BYTECODE_FLAG },
    { (char *)"shlex", NULL, 4870300, 7524, NUITKA_BYTECODE_FLAG },
    { (char *)"shutil", NULL, 4877824, 18543, NUITKA_BYTECODE_FLAG },
    { (char *)"site", NULL, 4896367, 16932, NUITKA_BYTECODE_FLAG },
    { (char *)"sitecustomize", NULL, 4913299, 224, NUITKA_BYTECODE_FLAG },
    { (char *)"smtpd", NULL, 4913523, 15813, NUITKA_BYTECODE_FLAG },
    { (char *)"smtplib", NULL, 4929336, 30169, NUITKA_BYTECODE_FLAG },
    { (char *)"sndhdr", NULL, 4959505, 7319, NUITKA_BYTECODE_FLAG },
    { (char *)"socket", NULL, 4966824, 16084, NUITKA_BYTECODE_FLAG },
    { (char *)"sqlite3", NULL, 4982908, 154, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"sqlite3.dbapi2", NULL, 4983062, 2682, NUITKA_BYTECODE_FLAG },
    { (char *)"sqlite3.dump", NULL, 4985744, 2057, NUITKA_BYTECODE_FLAG },
    { (char *)"sre", NULL, 4987801, 509, NUITKA_BYTECODE_FLAG },
    { (char *)"ssl", NULL, 4988310, 32213, NUITKA_BYTECODE_FLAG },
    { (char *)"stat", NULL, 5020523, 2723, NUITKA_BYTECODE_FLAG },
    { (char *)"statvfs", NULL, 5023246, 610, NUITKA_BYTECODE_FLAG },
    { (char *)"stringold", NULL, 5023856, 12487, NUITKA_BYTECODE_FLAG },
    { (char *)"subprocess", NULL, 5036343, 31531, NUITKA_BYTECODE_FLAG },
    { (char *)"sunau", NULL, 5067874, 18290, NUITKA_BYTECODE_FLAG },
    { (char *)"sunaudio", NULL, 5086164, 1969, NUITKA_BYTECODE_FLAG },
    { (char *)"symbol", NULL, 5088133, 3014, NUITKA_BYTECODE_FLAG },
    { (char *)"symtable", NULL, 5091147, 11678, NUITKA_BYTECODE_FLAG },
    { (char *)"sysconfig", NULL, 5102825, 18620, NUITKA_BYTECODE_FLAG },
    { (char *)"tabnanny", NULL, 5121445, 8195, NUITKA_BYTECODE_FLAG },
    { (char *)"tarfile", NULL, 5129640, 75786, NUITKA_BYTECODE_FLAG },
    { (char *)"telnetlib", NULL, 5205426, 23073, NUITKA_BYTECODE_FLAG },
    { (char *)"tempfile", NULL, 5228499, 20228, NUITKA_BYTECODE_FLAG },
    { (char *)"test", NULL, 5248727, 114, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"test.pystone", NULL, 5248841, 7981, NUITKA_BYTECODE_FLAG },
    { (char *)"textwrap", NULL, 5256822, 11996, NUITKA_BYTECODE_FLAG },
    { (char *)"this", NULL, 5268818, 1210, NUITKA_BYTECODE_FLAG },
    { (char *)"threading", NULL, 5270028, 42422, NUITKA_BYTECODE_FLAG },
    { (char *)"timeit", NULL, 5312450, 12149, NUITKA_BYTECODE_FLAG },
    { (char *)"tkColorChooser", NULL, 5324599, 1402, NUITKA_BYTECODE_FLAG },
    { (char *)"tkCommonDialog", NULL, 5326001, 1496, NUITKA_BYTECODE_FLAG },
    { (char *)"tkFileDialog", NULL, 5327497, 5121, NUITKA_BYTECODE_FLAG },
    { (char *)"tkFont", NULL, 5332618, 7076, NUITKA_BYTECODE_FLAG },
    { (char *)"tkMessageBox", NULL, 5339694, 3864, NUITKA_BYTECODE_FLAG },
    { (char *)"tkSimpleDialog", NULL, 5343558, 9053, NUITKA_BYTECODE_FLAG },
    { (char *)"toaiff", NULL, 5352611, 3090, NUITKA_BYTECODE_FLAG },
    { (char *)"token", NULL, 5355701, 3798, NUITKA_BYTECODE_FLAG },
    { (char *)"tokenize", NULL, 5359499, 14465, NUITKA_BYTECODE_FLAG },
    { (char *)"trace", NULL, 5373964, 22716, NUITKA_BYTECODE_FLAG },
    { (char *)"traceback", NULL, 5396680, 11631, NUITKA_BYTECODE_FLAG },
    { (char *)"ttk", NULL, 5408311, 62244, NUITKA_BYTECODE_FLAG },
    { (char *)"tty", NULL, 5470555, 1303, NUITKA_BYTECODE_FLAG },
    { (char *)"turtle", NULL, 5471858, 139094, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest", NULL, 5610952, 2954, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"unittest.case", NULL, 5613906, 40174, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.loader", NULL, 5654080, 11299, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.main", NULL, 5665379, 7970, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.result", NULL, 5673349, 7869, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.runner", NULL, 5681218, 7581, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.signals", NULL, 5688799, 2738, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.suite", NULL, 5691537, 10460, NUITKA_BYTECODE_FLAG },
    { (char *)"unittest.util", NULL, 5701997, 4494, NUITKA_BYTECODE_FLAG },
    { (char *)"urllib", NULL, 5706491, 50896, NUITKA_BYTECODE_FLAG },
    { (char *)"urllib2", NULL, 5757387, 47019, NUITKA_BYTECODE_FLAG },
    { (char *)"urlparse", NULL, 5804406, 14457, NUITKA_BYTECODE_FLAG },
    { (char *)"user", NULL, 5818863, 1714, NUITKA_BYTECODE_FLAG },
    { (char *)"uu", NULL, 5820577, 4294, NUITKA_BYTECODE_FLAG },
    { (char *)"uuid", NULL, 5824871, 22469, NUITKA_BYTECODE_FLAG },
    { (char *)"warnings", NULL, 5847340, 13431, NUITKA_BYTECODE_FLAG },
    { (char *)"wave", NULL, 5860771, 19903, NUITKA_BYTECODE_FLAG },
    { (char *)"weakref", NULL, 5880674, 15730, NUITKA_BYTECODE_FLAG },
    { (char *)"webbrowser", NULL, 5896404, 19713, NUITKA_BYTECODE_FLAG },
    { (char *)"whichdb", NULL, 5916117, 2229, NUITKA_BYTECODE_FLAG },
    { (char *)"wsgiref", NULL, 5918346, 719, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"wsgiref.handlers", NULL, 5919065, 16172, NUITKA_BYTECODE_FLAG },
    { (char *)"wsgiref.headers", NULL, 5935237, 7429, NUITKA_BYTECODE_FLAG },
    { (char *)"wsgiref.simple_server", NULL, 5942666, 6199, NUITKA_BYTECODE_FLAG },
    { (char *)"wsgiref.util", NULL, 5948865, 5961, NUITKA_BYTECODE_FLAG },
    { (char *)"wsgiref.validate", NULL, 5954826, 16767, NUITKA_BYTECODE_FLAG },
    { (char *)"xdrlib", NULL, 5971593, 9810, NUITKA_BYTECODE_FLAG },
    { (char *)"xml", NULL, 5981403, 1068, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"xml.dom", NULL, 5982471, 6427, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"xml.dom.NodeFilter", NULL, 5988898, 1112, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.dom.domreg", NULL, 5990010, 3293, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.dom.expatbuilder", NULL, 5993303, 32810, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.dom.minicompat", NULL, 6026113, 3394, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.dom.minidom", NULL, 6029507, 65141, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.dom.pulldom", NULL, 6094648, 12986, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.dom.xmlbuilder", NULL, 6107634, 15222, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.etree", NULL, 6122856, 119, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"xml.etree.ElementInclude", NULL, 6122975, 1951, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.etree.ElementPath", NULL, 6124926, 7560, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.etree.ElementTree", NULL, 6132486, 34794, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.etree.cElementTree", NULL, 6167280, 166, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.parsers", NULL, 6167446, 304, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"xml.parsers.expat", NULL, 6167750, 277, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.sax", NULL, 6168027, 3679, NUITKA_BYTECODE_FLAG | NUITKA_PACKAGE_FLAG },
    { (char *)"xml.sax._exceptions", NULL, 6171706, 6127, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.sax.expatreader", NULL, 6177833, 14701, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.sax.handler", NULL, 6192534, 12970, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.sax.saxutils", NULL, 6205504, 14685, NUITKA_BYTECODE_FLAG },
    { (char *)"xml.sax.xmlreader", NULL, 6220189, 19078, NUITKA_BYTECODE_FLAG },
    { (char *)"xmllib", NULL, 6239267, 26732, NUITKA_BYTECODE_FLAG },
    { (char *)"xmlrpclib", NULL, 6265999, 43824, NUITKA_BYTECODE_FLAG },
    { (char *)"zipfile", NULL, 6309823, 41487, NUITKA_BYTECODE_FLAG },
    { NULL, NULL, 0, 0, 0 }
};

void setupMetaPathBasedLoader( void )
{
    static bool init_done = false;

    if ( init_done == false )
    {
        registerMetaPathBasedUnfreezer( meta_path_loader_entries );
        init_done = true;
    }
}
